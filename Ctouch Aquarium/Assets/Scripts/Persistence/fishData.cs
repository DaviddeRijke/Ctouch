using System;
using System.Collections.Generic;

namespace Persistence
{
    [Serializable]
    public struct FishData
    {
        public string ModelUID;
        public string Name;
    }

    public struct ListContainer
    {
        public List<FishData> dataList;

        public ListContainer(List<FishData> _dataList)
        {
            dataList = _dataList;
        }
    }
}