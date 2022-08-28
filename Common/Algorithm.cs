namespace ExpSystem.Common
{
    public class Algorithm
    {
        public static void SortListsOfValue(ref List<string> fList, ref List<double> sList) {
            bool isSorting = true;
            while (isSorting) {
                isSorting = false;

                for(int i = 1; i < sList.Count; i++) {
                    if(sList[i] > sList[i - 1]) {
                        (sList[i], sList[i - 1]) = (sList[i - 1], sList[i]);
                        (fList[i], fList[i - 1]) = (fList[i - 1], fList[i]);
                        isSorting = true;
                    }
                }
            }
        }
    }
}