using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Reflection;

namespace API04.Models
{
    public class UserPreview
    {
        public string id;
        public string title;
        public string firstName;
        public string lastName;
        public string picture;
    }

    public class UserList
    {
        public UserPreview[] data;
        public int total;
        public int page;
        public int limit;
    }

    public class UserFull
    {
        public string id;
        public string title;
        public string firstName;
        public string lastName;
        public string picture;
        public string gender;
        public string email;
        public string dateOfBirth;
        public string registerDate;
        public string phone;
        public Location location;
    }

    public class Location
    {
        public string street;
        public string city;
        public string state;
        public string country;
        public string timezone;
    }
}
