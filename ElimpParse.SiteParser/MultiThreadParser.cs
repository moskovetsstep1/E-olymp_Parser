﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class MultiThreadParser
    {
        //TODO: убрать Action, нормлаьно реализовать нужный метод
        public static void MakeMultiThreadExecute(List<ElimpUser> userList, Action<ElimpUser> action)
        {
            var taskList = new List<Task>();
            foreach (var user in userList)
            {
                taskList.Add(Task.Run(() => TryLoad(user, action)));
            }
            Task.WaitAll(taskList.ToArray());
        }

        public static List<(ElimpUser, int)> LoadProblemsCount(List<ElimpUser> userList)
        {
            (ElimpUser, int) RecursiveExecute(ElimpUser user)
            {
                try
                {
                    return (user, Parser.CompletedTaskCount(user.Login));
                }
                catch
                {
                    return RecursiveExecute(user);
                }
            }

            var res = userList.AsParallel().Select(RecursiveExecute).ToList();
            return res;
        }

        private static void TryLoad(ElimpUser user, Action<ElimpUser> action)
        {
            try
            {
                action.Invoke(user);
            }
            catch (Exception e)
            {
                TryLoad(user, action);
            }
        }
    }
}