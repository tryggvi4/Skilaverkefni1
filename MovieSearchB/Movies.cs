using System;
using System.Collections.Generic;
namespace MovieSearchB
{
    public class Movies
    {
        private List<string> _persons;

        public Movies()
        {
            this._persons = new List<string>()
            {
                "Marilyn Monroe",
                "Abraham Lincoln",
                "John F. Kennedy",
                "Martin Luther King",
                "Nelson Mandela",
                "Winston Churchill",
                "Margaret Thatcher",
                "Muhammad Ali",
                "Bill Gates",
                "Mother Teresa",
                "Mahatma Gandhi",
                "Charles de Gaulle",
                "Christopher Columbus",
                "George Orwell",
                "Charles Darwin"
            };
        }

        public List<string> Persons => this._persons;
    }
}
