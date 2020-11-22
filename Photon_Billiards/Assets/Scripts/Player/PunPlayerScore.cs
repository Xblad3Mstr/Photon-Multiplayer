using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;

public class PunPlayerScore : MonoBehaviourPun
{
    public const string PlayerScoreProp = "score";
}

public static class ScoreExtensions
{
    public static void SetScore(this Player player, int newScore)
    {
        Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
        score[PunPlayerScore.PlayerScoreProp] = newScore;

        player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
    }

    public static void AddScore(this Player player, int scoreToAddToCurrent)
    {
        int current = player.GetScore();
        current = current + scoreToAddToCurrent;

        Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
        score[PunPlayerScore.PlayerScoreProp] = current;

        player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
    }

    public static int GetScore(this Player player)
    {
        object score;
        if (player.CustomProperties.TryGetValue(PunPlayerScore.PlayerScoreProp, out score))
        {
            return (int)score;
        }

        return 0;
    }
}
