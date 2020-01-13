using System.Collections.Generic;

namespace GitTask
{
    public class Git
    {
        private readonly int numberOfFiles;
        private int currentNumberCommit;
        private readonly List<List<(int, int)>> commits;
        public Git(int filesCount)
        {
            numberOfFiles = filesCount;
            currentNumberCommit = 0;
            commits = new List<List<(int, int)>>(capacity: filesCount);
            for (var i = 0; i < filesCount; i++)
                commits.Add(new List<(int, int)>());
        }
  
        public void Update(int fileNumber, int value)
        {
            var newUpdate = (currentNumberCommit, value);
            commits[fileNumber].Add(newUpdate);
        }

        public int Commit()
        {
            currentNumberCommit++;
            return currentNumberCommit-1;
        }
        
        private static int BinarySearch(int commitSouht, List<(int, int)> listCommitAndValue)
        {
            if (listCommitAndValue.Count == 0)
                return -1;
            
            var leftBorder = -1;
            var rightBorder =  listCommitAndValue.Count;
            var middle = (rightBorder + leftBorder) / 2;
            
            while ((commitSouht != listCommitAndValue[middle].Item1)&&(leftBorder < rightBorder - 1))
            {
                if (commitSouht > listCommitAndValue[middle].Item1)
                {
                    leftBorder = middle;
                }
                else
                    rightBorder = middle;
                
                middle = (rightBorder + leftBorder) / 2;
            }

            if (commitSouht != listCommitAndValue[middle].Item1)
                return leftBorder;
            else
                return middle;
        }

        public int Checkout(int commitNumber, int fileNumber)
        {
            if (commitNumber >= currentNumberCommit)
            {
                throw new System.ArgumentException("There was no commit this number.");
            }
            else
            {
                var indexOfSearchedUpdate = BinarySearch(commitNumber, commits[fileNumber]);
                
                if (indexOfSearchedUpdate == -1) 
                    return 0;
                
                return commits[fileNumber][indexOfSearchedUpdate].Item2;
            }
        }
    }
}