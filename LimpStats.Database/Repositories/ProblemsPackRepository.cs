﻿using System;
using System.Linq;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Database.Repositories
{
    public class ProblemsPackRepository
    {
        private readonly UserGroupRepository _userGroupRepository;

        public ProblemsPackRepository(UserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public void Create(string userGroupTitle, ProblemsPack problemsPack)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);

            if (userGroup.ProblemsPacks.Any(p => p.Title == problemsPack.Title))
            {
                throw new Exception("Pack with title already exist");
            }

            userGroup.ProblemsPacks.Add(problemsPack);
            _userGroupRepository.Update(userGroup);
        }

        public ProblemsPack Read(string userGroupTitle, string title)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);
            return userGroup.ProblemsPacks.First(p => p.Title == title);
        }

        public void Update(string userGroupTitle, ProblemsPack problemsPack)
        {
            Delete(userGroupTitle, problemsPack);
            Create(userGroupTitle, problemsPack);
        }

        public void Delete(string userGroupTitle, ProblemsPack problemsPack)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);

            int removedElementCount = userGroup.ProblemsPacks.RemoveAll(p => p.Title == problemsPack.Title);
            if (removedElementCount == 0)
            {
                throw new Exception("Group not found in json");
            }

            _userGroupRepository.Update(userGroup);
        }
    }
}