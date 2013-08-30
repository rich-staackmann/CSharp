using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTrackr.Models;

namespace TimeTrackr.DAL
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        Task GetTaskByID(int taskId);
        void InsertTask(Task Task, string userName);
        void DeleteTask(int TaskID);
        void UpdateTask(Task Task);
        void Save();
    }
}