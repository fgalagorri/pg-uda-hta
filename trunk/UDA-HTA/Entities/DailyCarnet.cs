using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DailyCarnet
    {
        private string _technical;
        private int _init_bp1;
        private int _init_bp2;
        private int _init_bp3;
        private int _init_hr1;
        private int _init_hr2;
        private int _init_hr3;
        private int _final_bp1;
        private int _final_bp2;
        private int _final_bp3;
        private int _final_hr1;
        private int _final_hr2;
        private int _final_hr3;
        private DateTime _begin_sleep_time;
        private DateTime _end_sleep_time;
        private string _how_sleep;
        private DateTime _main_meal_time;

        public string Technical 
        {
            get { return _technical; }
            set { _technical = value; }
        }

        public int Init_bp1
        {
            get { return _init_bp1; }
            set { _init_bp1 = value; }
        }

        public int Init_bp2
        {
            get { return _init_bp2; }
            set { _init_bp2 = value; }
        }

        public int Init_bp3
        {
            get { return _init_bp3; }
            set { _init_bp3 = value; }
        }

        public int Init_hr1
        {
            get { return _init_hr1; }
            set { _init_hr1 = value; }
        }

        public int Init_hr2
        {
            get { return _init_hr2; }
            set { _init_hr2 = value; }
        }

        public int Init_hr3
        {
            get { return _init_hr3; }
            set { _init_hr3 = value; }
        }

        public int Final_bp1
        {
            get { return _final_bp1; }
            set { _final_bp1 = value; }
        }

        public int Final_bp2
        {
            get { return _final_bp2; }
            set { _final_bp2 = value; }
        }

        public int Final_bp3
        {
            get { return _final_bp3; }
            set { _final_bp3 = value; }
        }

        public int Final_hr1
        {
            get { return _final_hr1; }
            set { _final_hr1 = value; }
        }

        public int Final_hr2
        {
            get { return _final_hr2; }
            set { _final_hr2 = value; }
        }

        public int Final_hr3
        {
            get { return _final_hr3; }
            set { _final_hr3 = value; }
        }

        public DateTime Begin_sleep_time
        {
            get { return _begin_sleep_time; }
            set { _begin_sleep_time = value; }
        }

        public DateTime End_sleep_time
        {
            get { return _end_sleep_time; }
            set { _end_sleep_time = value; }
        }

        public string How_sleep
        {
            get { return _how_sleep; }
            set { _how_sleep = value; }
        }

        public DateTime Main_meal_time
        {
            get { return _main_meal_time; }
            set { _main_meal_time = value; }
        }

    }
}
