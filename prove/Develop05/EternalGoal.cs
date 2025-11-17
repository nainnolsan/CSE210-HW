using System;

namespace Develop05
{
    class EternalGoal : Goal
    {
        public EternalGoal(string shortName, string description, int points) : base(shortName, description, points)
        {
            
        }

        public override int RecordEvent()
        {
            return GetPoints();
        }

        public override bool IsComplete()
        {
            return false;
        }

        public override string GetDetailsString()
        {
            return $"[ ] {GetShortName()} ({GetDescription()})";
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}";
        }
    }
}
