using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Doctors.Schedule
{
    public static class Save
    {
        public static async Task<LG.Data.Models.BaseModel> Batch(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Save.Batch(entity);
        }
        public static async Task<LG.Data.Models.BaseModel> OneDayBlock(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Save.OneDayBlock(entity);
        }
        public static async Task<LG.Data.Models.BaseModel> SingleBlock(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Save.SingleBlock(entity);
        }
    }
    public static class Load
    {
        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionerSchedule(
            LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Load.PractitionerSchedule(entity);
        }
        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionersOneState(
            LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Load.PractitionersOneState(entity);
        }
        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionerCountInRangeForAllStates(
           LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            return await LG.Data.Core.Doctors.Schedule.Load.PractitionerCountInRangeForAllStates(entity);
        }
        public static async Task<LG.Data.Models.Doctors.Schedule.DoctorsAvailablePerState> ListOfDoctorsPerState(
            string state)
        {
            return await LG.Data.Core.Doctors.Schedule.Load.ListOfDoctorsPerState(state);
        }
    }
    public static class Deactivate
    {
        public static async Task<bool> SingleBlock()
        {
            return await LG.Data.Core.Doctors.Schedule.Deactivate.SingleBlock();
        }
        public static async Task<bool> SingleBlockByID()
        {
            return await LG.Data.Core.Doctors.Schedule.Deactivate.SingleBlockByID();
        }
    }
}