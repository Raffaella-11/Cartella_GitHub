using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckManager : MonoBehaviour
{

	public static DeckManager instance;
    public Card[] deck;
	public Card[] fulldeck;
	private string[] Names_hard= new string[53]{"caffe", "cane", "cappello", "caraffa", "case", "castagna", "cavalli", "cavi", "chela", "chiave", "chiavi", "chiodi", "chitarra", "coccinella", "coccodrillo",  "coda",  "collo",  "colombo",  "corda",  "corvo",  "cozza",  "cuccia", "cuore", "cuscino", "gallina", "gambe", "gambero", "gambo", "ganci", "gatto", "ghepardo", "gheriglio", "ghiacciolo", "ghiande", "ghiro", "gobba", "gomitolo", "gomme", "gorilla", "gregge", "guanto", "gufo", "guinzaglio", "scacchi", "scacchiera", "scarpe", "scatola", "scheletro", "schiuma", "scoiattolo", "scopa", "scorpione", "scudo"};
	private string[] Names_soft= new string[21]{"cellula", "cerotto", "cervello", "ciabatta","cicogna", "cielo", "cigno", "cinghiale", "cintura",  "cipolla", "gelato", "gemelli", "gengiva", "genio", "gigante", "ginocchio", "giraffa", "gnomi", "sceriffo", "sciarpa", "scimmia"};
	
	public bool allowWordsC;
	public bool allowWordsG;
	public bool allowWordsSC;

	void Awake () {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }

        instance = this; 
        DontDestroyOnLoad(this.gameObject);


		allowWordsC = (PlayerPrefs.GetInt("GameAllowWordsC", 0)==1);
		allowWordsG = (PlayerPrefs.GetInt("GameAllowWordsG", 0)==1);
		allowWordsSC = (PlayerPrefs.GetInt("GameAllowWordsSC", 0)==1);
    }
	
    // Start is called before the first frame update
    void Start() {
	    deck = new Card[10];
        fulldeck = new Card[74];
		int index=0;
		for (int i=0; i<21; i++){
			Card temp = new Card();
			temp.idx = index;
			temp.name = Names_soft[i];
			temp.soft = true;
			temp.IsC = Names_soft[i].StartsWith("c");
			temp.IsG = Names_soft[i].StartsWith("g");
			temp.IsSC = Names_soft[i].StartsWith("sc");
			temp.artwork = Resources.Load<Sprite>(temp.name);
			temp.clip = Resources.Load<AudioClip>("Audio/" + temp.name);
            fulldeck[index] = temp;
            index++;
            //Debug.Log(Names_soft[i].StartsWith("c");
		}	
		for (int i=0; i<53; i++){
			Card temp = new Card();
			temp.idx = index;
			temp.name = Names_hard[i];
			temp.soft = false;
			temp.IsC = Names_hard[i].StartsWith("c");
			temp.IsG = Names_hard[i].StartsWith("g");
			temp.IsSC = Names_hard[i].StartsWith("sc");
			temp.artwork = Resources.Load<Sprite>(temp.name);
			temp.clip = Resources.Load<AudioClip>("Audio/" + temp.name);
			fulldeck[index] = temp;
			index++;
			//Debug.Log(Names_hard[i].StartsWith("c");
		}

		Shuffle();
    }

    

    public void Shuffle()
    {
	    int replacements = UnityEngine.Random.Range(100, 1000);
	    for (int i=0; i < replacements; i++){
		    int A = UnityEngine.Random.Range(0, 74);
		    int B = UnityEngine.Random.Range(0, 74);

		    Card a = fulldeck[A];
		    Card b = fulldeck[B];
		    Card c = fulldeck[A];

		    a = b;
		    b = c;

		    fulldeck[A] = a;
		    fulldeck[B] = b;
	    }

		// da eliminare quando mettiamo a posto i pulsanti
		allowWordsC = true;
		allowWordsG = true;
		allowWordsSC = true;


		for (int i = 0; i < 10; i++) {
			deck[i] = fulldeck[i];
		}

		/*
		// non funzia
		int n = 0;
		int m = 0;
		while (n < 10 && m < 73) {
			if (allowWordsC & fulldeck[m].IsC) {
				deck[n] = fulldeck[m];
				n++;
			} else if (allowWordsG & fulldeck[m].IsG) {
				deck[n] = fulldeck[m];
				n++;
			} else if (allowWordsSC & fulldeck[m].IsSC) {
				deck[n] = fulldeck[m];
				n++;
			}
			m++;
		}
		*/
	}

	public void ToggleWordsC() {
		allowWordsC = !allowWordsC;
		PlayerPrefs.SetInt("GameAllowWordsC", allowWordsC?1:0);
	}
	public void ToggleWordsG() {
		allowWordsG = !allowWordsG;
		PlayerPrefs.SetInt("GameAllowWordsG", allowWordsC?1:0);
	}
	public void ToggleWordsSC() {
		allowWordsSC = !allowWordsSC;
		PlayerPrefs.SetInt("GameAllowWordsSC", allowWordsC?1:0);
	}

	

	
	
}

[Serializable]
public class Card
{
	public int idx;
   	public string name; 
	public bool soft;
	public Sprite artwork;
	public AudioClip clip;
	public bool IsC;
	public bool IsG;
	public bool IsSC;

}



