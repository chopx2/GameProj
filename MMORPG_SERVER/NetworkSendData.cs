﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMORPG_SERVER
{
    class NetworkSendData
    {
        public void SendDataTo(int index, byte[] data)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);
            Globals.Clients[index].myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer = null;
        }

        public async void SendDataToAll(byte[]data)
        {
            for(int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if(Globals.Clients[i].Socket != null)
                {
                    await Task.Delay(1000);
                    SendDataTo(i, data);
                }
            }
        }

        public void SendDataToAllBut(int index, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    if (i != index)
                    {
                        SendDataTo(i, data);
                    }
                }
            }
        }

        public void SendJoinGame(int index)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger(1);
            buffer.WriteInteger(index);
            SendDataTo(index, buffer.ToArray());
            ////Sends the player to the others
            SendInstantiatePlayer(index);
            ////Sends other players to the index player
            SendGetOtherPlayer(index);

        }

        public async void SendInstantiatePlayer(int index)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger(2);
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    if (i != index)
                    {
                        foreach(var value in Globals.Clients)
                        {
                            buffer.WriteInteger(i);
                            await Task.Delay(1000);
                            SendDataTo(i, buffer.ToArray());
                        }
                    }
                }
            }
        }

        public async void SendGetOtherPlayer(int index)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger(3);

            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    if(i != index)
                    {
                        buffer.WriteInteger(i);
                        await Task.Delay(1000);
                        SendDataTo(index, buffer.ToArray());
                    }
                }
            }
        }
    }
}
