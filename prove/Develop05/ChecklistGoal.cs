using System;

namespace Develop05
{
    class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _bonus;
        private int _currentCount;

        public ChecklistGoal(string shortName, string description, int points, int targetCount, int bonus) : base(shortName, description, points)
        {
            _targetCount = targetCount;
            _bonus = bonus;
            _currentCount = 0;
        }

        public override int RecordEvent()
        {
            _currentCount++;
            int total = GetPoints();
            if (_currentCount == _targetCount)
            {
                total += _bonus;
            }
            return total;
        }

        public override bool IsComplete()
        {
            return _currentCount >= _targetCount;
        }

        public override string GetDetailsString()
        {
            string status = IsComplete() ? "X" : " ";
            return $"[{status}] {GetShortName()} ({GetDescription()}) -- Currently completed {_currentCount}/{_targetCount}";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}|{_targetCount}|{_bonus}|{_currentCount}";
        }

        public void SetCurrentCount(int count)
        {
            _currentCount = count;
        }
    }
}
