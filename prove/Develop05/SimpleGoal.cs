using System;

namespace Develop05
{
    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string shortName, string description, int points) : base(shortName, description, points)
        {
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return GetPoints();
            }
            return 0;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        public override string GetDetailsString()
        {
            string status = _isComplete ? "X" : " ";
            return $"[{status}] {GetShortName()} ({GetDescription()})";
        }

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}|{_isComplete}";
        }

        public void MarkComplete()
        {
            _isComplete = true;
        }
    }
}
