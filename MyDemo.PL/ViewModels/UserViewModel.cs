using System;
using System.Collections.Generic;

namespace MyDemo.PL.ViewModels
{
	public class UserViewModel
	{
        public string Id { get; set; } //becauese Identity generate it by GUID
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }


        ///If U want to Create Action to Create New User U should initailize Id to be GUID in the Contructor
        ///public UserViewModel()
        ///{
        ///    Id = Guid.NewGuid().ToString();
        ///}

    }
}
