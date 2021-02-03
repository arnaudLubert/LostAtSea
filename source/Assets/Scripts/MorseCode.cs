using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip toneShort;
    public AudioClip toneLong;

    private Communication communication;
    private Queue<char> morseBuffer;

    private string[] morseCode = {
        " ", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        // SPACE !     "             $              &        '         (        )             +        ,         .         -         /
        " ", "-.-.--", ".-..-.", "", "...-..-", "", ".-...", ".----.", "-.--.", "-.--.-", "", ".-.-.", "--..--", ".-.-.-", "-....-", "-..-.",
        // 0     1        2        3        4        5        6        7        8        9        :         ;             =            ?
        "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----.", "---...", "-.-.-.", "", "-...-", "", "..--..",
        // @      A     B       C       D      E    F       G      H       I     J       K      L       M     N     O
        ".--.-.", ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---",
        // P    Q       R      S      T    U      V       W      X       Y       Z                       _
        ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", "", "", "", "", "..--.-",
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
    };

    // Start is called before the first frame update
    void Start()
    {
        communication = GetComponent<Communication>();
        morseBuffer = new Queue<char>();
        StartCoroutine("MorseTone");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    public void SendMorse(string message)
    {
        communication.SendMsg(message);
        ReadMessage(message);
    }

    public void ReadMessage(string message)
    {
        morseBuffer.Clear();
        message = message.ToUpper();
        //Debug.Log("Process morse message: " + message);
        foreach (char c in message)
        {
            //Debug.Log("Append letter in morse buffer: " + c + " = " + morseCode[c]);
            foreach (char tone in morseCode[c])
            {
                morseBuffer.Enqueue(tone);
            }

            morseBuffer.Enqueue(' ');
        }
    }

        //
        IEnumerator MorseTone()
    {
        float length = 0.0F;

        while (true)
        {
            if (morseBuffer.Count > 0)
            {
                char tone = morseBuffer.Dequeue();

                AudioClip audioClip = null;
                switch (tone)
                {
                    case '.':
                        audioClip = toneShort;
                        length = 1.5f;
                        break;

                    case '-':
                        audioClip = toneLong;
                        length = 5;
                        break;

                    case ' ':
                    default:
                        length = 5;
                        break;
                }

                if (audio != null && audioClip != null)
                {
                    audio.clip = audioClip;
                    audio.Play();
                }
            }/*
            else
            {
                Write("SOS   ");
            }*/

            yield return new WaitForSeconds(.03f * (1 + length));
        }
    }
}
