using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trackerScript : MonoBehaviour
{   
    public int[] p1RosterT;
    public int[] p2RosterT;
    public Text p1roster;
    public Text p2roster;
    public Text playerchoicetext;
    public Text p1candytext;
    public Text p2candytext;
    public int[] rosterOne;
    public int[] rosterTwo;
    public bool p1turn;
    public int[] priceGuide;
    public string[] kidNames;
    public int p1candy;
    public int p2candy;

    public AudioSource swapSound;
    private void Awake()
    {
        
        // modifiable design parameters
        this.priceGuide = new int[] { 5, 8, 7, 8, 8 };
        this.kidNames = new string[] {"regular", "sniper", "runner", "catcher", "diagonal"};
        int candyamt = 60;
        int rosterSize = 10;

        this.rosterOne = new int[rosterSize];
        this.rosterTwo = new int[rosterSize];
        for (int i = 0; i< rosterOne.Length; i++) {
            rosterOne[i] = 0;
            rosterTwo[i] = 0;
        }
        this.p1turn = true;
        this.p1candy = candyamt;
        this.p2candy = candyamt;
        this.playerchoicetext.text = "Player 1 choosing!";
        this.playerchoicetext.color = Color.blue;
        this.p1RosterT = new int[] {0,0,0,0,0};
        this.p2RosterT = new int[] {0,0,0,0,0};

        setUITexts();
        //play music
    }

    public void undo(int roster) {
        if (roster == 1) {
            int lastIndex = 0; // find most recently added index
            for (int i = 0; i < rosterOne.Length; i++) {
                if (rosterOne[i] == 0) {
                    lastIndex = i - 1;
                    if (lastIndex >= 0) { // catch for empty roster
                        p1candy += priceGuide[rosterOne[lastIndex] - 1]; // find which kid in last index and refund price
                        rosterOne[lastIndex] = 0;
                    }
                    break;
                }
            }

            p1turn = true;
        } else if (roster == 2) {
            int lastIndex = 0; // find most recently added index
            for (int i = 0; i < rosterTwo.Length; i++) {
                if (rosterTwo[i] == 0) {
                    lastIndex = i - 1;
                    if (lastIndex >= 0) { // catch for empty roster
                        p2candy += priceGuide[rosterTwo[lastIndex] - 1]; // find which kid in last index and refund price
                        rosterTwo[lastIndex] = 0;
                    }
                    break;
                }
            }

            p1turn = false;
        }
        
        setUITexts();
    }

    public void transformRosters () {
        // Q: {"Normal", "Sniper", "Runner", "Catcher", "TwoBalls"};
        // J/E: {"Normal", "Sniper", "Catcher", "Twoballs", "Runner"}
        for (int i = 0; i < rosterOne.Length; i++) {
            if (rosterOne[i] == 1) {
                p1RosterT[0] += 1;
            } else if (rosterOne[i] == 2) {
                p1RosterT[1] += 1;
            } else if (rosterOne[i] == 3) {
                p1RosterT[4] += 1;
            } else if (rosterOne[i] == 4) {
                p1RosterT[2] += 1;
            } else if (rosterOne[i] == 5) {
                p1RosterT[3] += 1;
            }

            if (rosterTwo[i] == 1) {
                p2RosterT[0] += 1;
            } else if (rosterTwo[i] == 2) {
                p2RosterT[1] += 1;
            } else if (rosterTwo[i] == 3) {
                p2RosterT[4] += 1;
            } else if (rosterTwo[i] == 4) {
                p2RosterT[2] += 1;
            } else if (rosterTwo[i] == 5) {
                p2RosterT[3] += 1;
            }
        }

        GiveMeArrays.player1Stocks = p1RosterT;
        GiveMeArrays.player2Stocks = p2RosterT;
    }

    public void setUITexts() {
        p1candytext.text = p1candy.ToString();
        p2candytext.text = p2candy.ToString();
        if (p1turn) {
            playerchoicetext.text = "Player 1 choosing!";
            playerchoicetext.color = Color.blue;
            swapSound.Play();
        } else {
            playerchoicetext.text = "Player 2 choosing!";
            playerchoicetext.color = Color.red;
            swapSound.Play();
        }
        p1roster.text = "";
        p2roster.text = "";
        for (int i = 0; i < rosterOne.Length; i++) {
            if (rosterOne[i] != 0) {
                p1roster.text += ", " + kidNames[rosterOne[i] - 1];
                
            }
            if (rosterTwo[i] != 0) {
                p2roster.text += ", " + kidNames[rosterTwo[i] - 1];
            }
        }
        if (p1roster.text.Length > 0) {
            p1roster.text = p1roster.text.Substring(2);
        }
        if (p2roster.text.Length > 0) {
            p2roster.text = p2roster.text.Substring(2);
        }
    }

    public void kidRecruit(int kid) {
        // check player can afford kid
        if (p1turn && p1candy >= priceGuide[kid]) {
            p1candy -= priceGuide[kid];
            AddToRoster1(kid + 1);
            if (p2candy >= 5) { // p2 has enough to hire
                p1turn = false;
            }
        }
        else if (!p1turn && p2candy >= priceGuide[kid]) {
            p2candy -= priceGuide[kid];
            AddToRoster2(kid + 1);
            if (p1candy >= 5) {
                p1turn = true;
            }
        }

        Debug.Log("p1candy = " + p1candy);
        Debug.Log("p2candy = " + p2candy);
    }

    public void buttonResolve(int kid) // 1-5
    {
        // call recruit
        kidRecruit(kid - 1); // 0-4

        // debug
        string pstr1 = "r1 = ";
        string pstr2 = "r2 = ";

        for (int i = 0; i < rosterOne.Length; i++) {
            pstr1 += rosterOne[i] + ", ";
            pstr2 += rosterTwo[i] + ", ";
        }

        Debug.Log(pstr1);
        Debug.Log(pstr2);

        setUITexts();
    }

    private void AddToRoster1(int kid)
    {
        if (rosterOne[6] == 0)
        { // roster is not full
            for (int i = 0; i < rosterOne.Length; i++)
            { // find next empty slot and add kid
                if (rosterOne[i] == 0)
                {
                    rosterOne[i] = kid;
                    break;
                }
            }
        }
    }

    private void AddToRoster2(int kid)
    {
        if (rosterTwo[6] == 0)
        { // roster is not full
            for (int i = 0; i < rosterTwo.Length; i++)
            { // find next empty slot and add kid
                if (rosterTwo[i] == 0)
                {
                    rosterTwo[i] = kid;
                    break;
                }
            }
        }
    }
}
