using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleDungeonGenerator
{
	public class DungeonGenerator2D : MonoBehaviour
	{
		[SerializeField]
		int minPathLength = 2;
		[SerializeField]
		int maxPathLength = 3;
		private void Start()
		{
			GenerateDungeon();
		}
		public bool HasNeighborRoom(List<DungeonRoom> rooms, DungeonRoom current, int direction)
		{
			Vector2 targetCoord = current.coordinate;
			if (direction == 0)
			{
				targetCoord = new Vector2(targetCoord.x, targetCoord.y + 1);
			}
			else if (direction == 1)
			{
				targetCoord = new Vector2(targetCoord.x + 1, targetCoord.y);
			}
			else if (direction == 2)
			{
				targetCoord = new Vector2(targetCoord.x, targetCoord.y - 1);
			}
			else
			{
				targetCoord = new Vector2(targetCoord.x - 1, targetCoord.y);
			}

			foreach (DungeonRoom room in rooms)
			{
				if (room != current)
				{
					if (room.coordinate == targetCoord)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void GenerateDungeon()
		{
			int pathLengh = Random.Range(minPathLength, maxPathLength + 1);

			// entry room with all 4 walls passable
			DungeonRoom entry = new DungeonRoom();
			entry.coordinate = Vector2.zero;
			List<DungeonRoom> allRooms = new List<DungeonRoom> { entry };

			// generation algo
			Queue<DungeonRoom> roomsQ = new Queue<DungeonRoom>();
			roomsQ.Enqueue(entry);

			for (int i = 0; i < pathLengh; i++)
			{
				List<DungeonRoom> newRooms = new List<DungeonRoom>();
				while (roomsQ.Count > 0)
				{
					DungeonRoom currentRoom = roomsQ.Dequeue();
					// for each passable wall
					for (int j = 0; j < 4; j++)
					{
						if (!currentRoom.HasWallTRBL[j] && !HasNeighborRoom(allRooms, currentRoom, j))
						{
							DungeonRoom newRoom = new DungeonRoom(true);
							int oppositeDirection = (j + 2) % 4;
							if (oppositeDirection == 0)
							{
								newRoom.coordinate = new Vector2(currentRoom.coordinate.x, currentRoom.coordinate.y - 1);
							}
							else if (oppositeDirection == 1)
							{
								newRoom.coordinate = new Vector2(currentRoom.coordinate.x - 1, currentRoom.coordinate.y);
							}
							else if (oppositeDirection == 2)
							{
								newRoom.coordinate = new Vector2(currentRoom.coordinate.x, currentRoom.coordinate.y + 1);
							}
							else
							{
								newRoom.coordinate = new Vector2(currentRoom.coordinate.x + 1, currentRoom.coordinate.y);
							}

							newRoom.HasWallTRBL[oppositeDirection] = false;
							newRoom.NeighbourRoomsTRBL[oppositeDirection] = currentRoom;
							// make random entry
							if (i < pathLengh)
							{
								for (int k = 0; k < 4; k++)
								{
									if (k != oppositeDirection)
									{
										newRoom.HasWallTRBL[k] = Random.Range(0, 2) == 1;
									}
								}
							}
							currentRoom.NeighbourRoomsTRBL[j] = newRoom;
							allRooms.Add(newRoom);
							newRooms.Add(newRoom);
						}
					}
				}
				newRooms.ForEach(r => roomsQ.Enqueue(r));
				newRooms.Clear();
			}
		}
	}
	public class DungeonRoom
	{
		public bool[] HasWallTRBL = new bool[4] { false, false, false, false };
		public DungeonRoom[] NeighbourRoomsTRBL = new DungeonRoom[4] { null, null, null, null };
		public Vector2 coordinate;
		public DungeonRoom()
		{

		}
		public DungeonRoom(bool walls)
		{
			HasWallTRBL = new bool[4] { walls, walls, walls, walls };
		}
		public DungeonRoom(bool[] wallList)
		{
			HasWallTRBL = wallList;
		}
	}
}