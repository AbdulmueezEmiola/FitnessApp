using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Services
{
    public interface IDataStoreClient
    {
        Task<bool> WriteRecord(FitnessRecord newRecord);
        Task<List<FitnessRecord>> GetAllRecords();
        Task<List<FitnessRecord>> GetRecordsByWorkoutType(string workoutType);
        Task<FitnessRecord> GetRecordByTitle(string title);
        Task<bool> UpdateRecord(string title, string newComment);
        Task<bool> DeleteRecord(String title);
    }
}
