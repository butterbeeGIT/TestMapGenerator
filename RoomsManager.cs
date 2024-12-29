using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMapGenerator
{
    /// <summary>
    /// Управляет связями между комнатами 
    /// </summary>
    public class RoomsManager
    {
        private List<Room> _rooms;

        public void RoomManager(List<Room> rooms)
        {
            _rooms = rooms;
            DefinesNeighbors();
        }

        /// <summary>
        /// формирует списки соседних комнат
        /// </summary>
        private void DefinesNeighbors()
        {

        }

        /// <summary>
        /// Возвращает список ближайших комнат к заданной
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        //private List<Room> NextNearestRoom(Room room)
        //{

        //}
    }
}
