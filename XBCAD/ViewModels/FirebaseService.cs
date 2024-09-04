using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XBCAD.ViewModels;

public class FirebaseService
{
    private readonly FirebaseClient firebase;

    public FirebaseService()
    {
        // Ensure this URL is correct and points to your Firebase Realtime Database
        firebase = new FirebaseClient("https://alleysway-310a8-default-rtdb.firebaseio.com/");
    }

    public async Task<AvailabilityViewModel> GetAvailabilityAsync()
    {
        var daysSnapshot = await firebase
            .Child("Days")
            .OnceAsync<DayAvailability>();

        // Check if the snapshot is empty and create default days if necessary
        if (daysSnapshot == null || !daysSnapshot.Any())
        {
            return new AvailabilityViewModel
            {
                Days = new List<DayAvailability>
                {
                    new DayAvailability { Day = "Monday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Tuesday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Wednesday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Thursday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Friday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Saturday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Sunday", TimeSlots = new List<TimeSlot>() }
                }
            };
        }

        return new AvailabilityViewModel
        {
            Days = daysSnapshot.Select(d => new DayAvailability
            {
                Day = d.Key,
                TimeSlots = d.Object.TimeSlots
            }).ToList()
        };
    }

    public async Task SaveTimeSlotAsync(string day, string startTime, string endTime)
    {
        var timeSlot = new TimeSlot { StartTime = startTime, EndTime = endTime };
        await firebase
            .Child("Days")
            .Child(day)
            .Child("TimeSlots")
            .PostAsync(timeSlot);
    }

    public async Task RemoveTimeSlotAsync(string day, string startTime, string endTime)
    {
        var slots = await firebase
            .Child("Days")
            .Child(day)
            .Child("TimeSlots")
            .OnceAsync<TimeSlot>();

        var slotToRemove = slots.FirstOrDefault(ts => ts.Object.StartTime == startTime && ts.Object.EndTime == endTime);
        if (slotToRemove != null)
        {
            await firebase
                .Child("Days")
                .Child(day)
                .Child("TimeSlots")
                .Child(slotToRemove.Key)
                .DeleteAsync();
        }
    }
}