using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int p1rostercount;
    public int p2rostercount;
    public Text p1rostercountUI;
    public Text p2rostercountUI;

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
        this.p1rostercount = 0;
        this.p2rostercount = 0;

        setUITexts();
        //play music
    }

    public void undo(int roster) {
        if (roster == 1) {
            int lastIndex = 0; // find most recently added index
            if (rosterOne[rosterOne.Length - 1] != 0) { // if roster full
                lastIndex = rosterOne.Length - 1; // we know is in last slot
                p1candy += priceGuide[rosterOne[lastIndex] - 1]; // refund end roster kid
                rosterOne[lastIndex] = 0;
                p1rostercount--;
            } else if (rosterOne[0] == 0) {
                // empty, nothing to undo
            } else { // roster partially full
                for (int i = 0; i < rosterOne.Length; i++) { // find last index
                    if (rosterOne[i] == 0) {
                        lastIndex = i - 1;
                        p1candy += priceGuide[rosterOne[lastIndex] - 1]; // find which kid in last index and refund price
                        rosterOne[lastIndex] = 0;
                        p1rostercount--;
                        break;
                    }
                }
            }
            p1turn = true;

        } else if (roster == 2) {
            int lastIndex = 0; // find most recently added index
            if (rosterTwo[rosterTwo.Length - 1] != 0) { // if roster full
                lastIndex = rosterTwo.Length - 1; // we know is in last slot
                p2candy += priceGuide[rosterTwo[lastIndex] - 1]; // refund end roster kid
                rosterTwo[lastIndex] = 0;
                p2rostercount--;
            } else if (rosterTwo[0] == 0) {
                // empty, nothing to undo
            } else { // roster partially full
                for (int i = 0; i < rosterTwo.Length; i++) { // find last index
                    if (rosterTwo[i] == 0) {
                        lastIndex = i - 1;
                        p2candy += priceGuide[rosterTwo[lastIndex] - 1]; // find which kid in last index and refund price
                        rosterTwo[lastIndex] = 0;
                        p2rostercount--;
                        break;
                    }
                }
            }

            p1turn = false;
        }
        
        setUITexts();
    }

    public void transformRosters () {
        if (!(rosterOne[0] == 0) && !(rosterTwo[0] == 0)) { // if one or both players have made no selections

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

        SceneManager.LoadScene(2);
        
        }
    }

    public void setUITexts() {
        p1candytext.text = p1candy.ToString();
        p2candytext.text = p2candy.ToString();
        p1rostercountUI.text = p1rostercount.ToString();
        p2rostercountUI.text = p2rostercount.ToString();

        if (p1turn) {
            playerchoicetext.text = "Player 1 choosing!";
            playerchoicetext.color = Color.red;
            swapSound.Play();
        } else {
            playerchoicetext.text = "Player 2 choosing!";
            playerchoicetext.color = Color.blue;
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
        // check player can afford kid, and that roster isn't full
        if (p1turn && p1candy >= priceGuide[kid] && rosterOne[rosterOne.Length - 1] == 0) {
            p1candy -= priceGuide[kid];
            AddToRoster1(kid + 1);
            p1rostercount++;
            if (p2candy >= 5) { // p2 has enough to hire
                p1turn = false;
            }
        }
        else if (!p1turn && p2candy >= priceGuide[kid] && rosterTwo[rosterTwo.Length - 1] == 0) {
            p2candy -= priceGuide[kid];
            AddToRoster2(kid + 1);
            p2rostercount++;
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
            for (int i = 0; i < rosterOne.Length; i++)
            { // find next empty slot and add kid
                if (rosterOne[i] == 0)
                {
                    rosterOne[i] = kid;
                    break;
                }
            }
    }

    private void AddToRoster2(int kid)
    {
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
