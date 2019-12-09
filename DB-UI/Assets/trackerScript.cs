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

    private void Awake()
    {
        this.rosterOne = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        this.rosterTwo = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        this.p1turn = true;
        this.p1candy = 35;
        this.p2candy = 35;
        this.playerchoicetext.text = "Player 1 choosing!";
        this.playerchoicetext.color = Color.blue;
        this.p1RosterT = new int[] {};
        this.p2RosterT = new int[] {};

        // modifiable design parameters
        this.priceGuide = new int[] { 5, 8, 7, 10, 8 };
        this.kidNames = new string[] {"Normal", "Sniper", "Runner", "Catcher", "TwoBalls"};
    }

    public void undo(int roster) {
        if (roster == 1) {
            int lastIndex = 0; // find most recently added index
            for (int i = 0; i < 7; i++) {
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
            for (int i = 0; i < 7; i++) {
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
        transformRosters();
    }

    public void transformRosters () {
        for (int i = 0; i < 7; i++) {
            if (rosterOne[i] != 0) { // empty slot protect
                p1RosterT[rosterOne[i] - 1] += 1;
            }
            if (rosterTwo[i] != 0) {
                p2RosterT[rosterTwo[i] - 1] += 1;
            }
        }
    }

    public void setUITexts() {
        p1candytext.text = p1candy.ToString();
        p2candytext.text = p2candy.ToString();
        if (p1turn) {
            playerchoicetext.text = "Player 1 choosing!";
            playerchoicetext.color = Color.blue;
        } else {
            playerchoicetext.text = "Player 2 choosing!";
            playerchoicetext.color = Color.red;
        }
        p1roster.text = "";
        p2roster.text = "";
        for (int i = 0; i < 7; i++) {
            if (rosterOne[i] != 0) {
                p1roster.text += kidNames[rosterOne[i] - 1] + "\n";
                
            }
            if (rosterTwo[i] != 0) {
                p2roster.text += kidNames[rosterTwo[i] - 1] + "\n";
            }
        }
        
    }

    public void kidRecruit(int kid) {
        // check player can afford kid
        if (p1turn && p1candy >= priceGuide[kid]) {
            p1candy -= priceGuide[kid];
            AddToRoster1(kid + 1);
            p1turn = false;
        }
        else if (!p1turn && p2candy >= priceGuide[kid]) {
            p2candy -= priceGuide[kid];
            AddToRoster2(kid + 1);
            p1turn = true;
        }

        Debug.Log("p1candy = " + p1candy);
        Debug.Log("p2candy = " + p2candy);
    }

    public void buttonResolve(int kid)
    {
        // call recruit
        kidRecruit(kid - 1);

        // debug
        string pstr1 = "r1 = ";
        string pstr2 = "r2 = ";

        for (int i = 0; i < 7; i++) {
            pstr1 += rosterOne[i] + ", ";
            pstr2 += rosterTwo[i] + ", ";
        }

        Debug.Log(pstr1);
        Debug.Log(pstr2);

        setUITexts();
        transformRosters();
    }

    private void AddToRoster1(int kid)
    {
        if (rosterOne[6] == 0)
        { // roster is not full
            for (int i = 0; i < 7; i++)
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
            for (int i = 0; i < 7; i++)
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
