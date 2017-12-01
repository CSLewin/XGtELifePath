using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharGen : MonoBehaviour {

	public Text displayText = null;

	public int flip;

	public string playerGender = "testgender";
	public string playerRace = "testrace";
	public string playerClass = "testclass";
	public string playerSubclass = "testsubclass";
	public int childhoodMemoriesModifier = 0;
	public string playerBackground = "testbackground";
	public int playerBackgroundSpecialResult;
	public string playerBackgroundSpecial = null;
	public string playerPersonality; 
	public string playerIdeal;
	public string playerBond;
	public string playerFlaw;

	public string classQuirk1; 
	public string classQuirk2; 
	public string classQuirk3;
	public string classQuirk4; //only used by Bards, Paladins, and Sorcerers. I guess they're extra quirky.

	public string playerParentPresence;
	public bool parentsKnown;
	public string mixedAncestry = null;
	public string playerMotherDetails;
	public string playerFatherDetails;

	public string playerBirthplace;

	public int playerNumberOfSiblings;
	public string birthOrder;
	public string siblingText;

	public string playerFamily;
	public string causeOfDeath;
	public string absentParent;

	public string playerLifestyle;
	public int childhoodHomeModifier;
	public string playerHome;

	public string playerChildhoodMemories;

	public string playerBackgroundPersonalDecision;
	public string playerClassPersonalDecision;

	public int playerAge;
	public int playerLifeEventCount;
	public string lifeEventText;
	public int lifeEventResult;
	public string lifeEventEnemyBlame;

	public string AdventureText;
	public string ArcaneMattersText;
	public string BoonsText;
	public string CrimeText;
	public string PunishmentText;
	public string SupernaturalEventsText;
	public string TragediesText;
	public string WarText;
	public string WeirdStuffText;

	public string missingBodyPart;
	public string adventureDisease;
	public string adventurePoisonSource;
	public string arcaneMattersCaster;
	public string punishmentImprisonmentType;
	public string punishmentServedTime;
	public string supernaturalEventsPossession;
	public string supernaturalEventsDreamVisit;
	public string supernaturalEventsPlanarVisit;
	public string tragedyImprisonmentType;
	public string tragedyCommunityDestructionAftermath;
	public string tragedyExile;
	public string tragedyEndOfFriendship;
	public string tragedyEndOfRomance;
	public string weirdStuffCaptors;
	public string weirdStuffMetAGod;

	public string grammarPrintLifeEvents;
	public string grammarRollSiblings;

	void Awake () {
		displayText.text = null;
	}

	// Use this for initialization
	void Start () {
		displayText.text += "Press 'A' to generate a new character.\n\n";
		displayText.text += "<i>(Created with deep inspiration from--and tremendous thanks to--WotC and the XGtE creators.)</i>";

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			flip = roll(1,2);

			//reset the display for optional character details when generating a new character, or else the old stuff sticks around if nothing replaces it.
			childhoodMemoriesModifier = 0;
			playerBackgroundSpecial = null;
			mixedAncestry = null;
			playerMotherDetails = null;
			playerFatherDetails = null;
			siblingText = null;
			causeOfDeath = null;
			lifeEventText = null;
			classQuirk4 = null;

			GenerateCharacter ();

			displayText.text = "Press 'A' to generate a new character.\n\n";

			displayText.text += "You are a " + playerGender + " " + playerRace + " " + playerClass + playerSubclass + "! You are " + playerAge + " years old.\n\n";

			displayText.text += "Prior to adventuring, you were " + playerBackground + ". " + playerBackgroundPersonalDecision + "\n";
			displayText.text += playerBackgroundSpecial + "\n";

			displayText.text += "Personality: " + playerPersonality + "\n";
			displayText.text += "Ideal: " + playerIdeal + "\n";
			displayText.text += "Flaw: " + playerFlaw + "\n\n";

			displayText.text += playerParentPresence + " " + mixedAncestry + playerMotherDetails + " " + playerFatherDetails + " You were born " + playerBirthplace + " You were raised " + RollFamily() + 
			" Your lifestyle was " + playerLifestyle + ", and you lived " + playerHome + " " + playerChildhoodMemories + "\n\n";

			displayText.text += RollSiblings () + "\n";

			displayText.text += "You took up the life of a " + playerClass + " because " + playerClassPersonalDecision + " " + RollClassQuirks() + "\n\n";

			displayText.text += PrintLifeEvents() + "\n";
				

		}
	}

	void GenerateCharacter () {
		GenerateBasics ();
		GenerateOrigins ();
		GeneratePersonalDecisions ();
		GenerateLifeEvents ();
	}

	int roll (int dicepool, int sides) {
		int dicepoolDisplay = dicepool;
		int result = 0;
		int tempresult;
		for (int i = 1; i <= dicepool; i++)
		{
			tempresult = Random.Range (1, sides + 1);
			result += tempresult;
			Debug.Log(dicepoolDisplay + "d" + sides + ": Rolled 1d" + sides + ", got " + tempresult + ", current total is " + result + ".");
		}
		Debug.Log ("Rolls complete. Result: " + result);
		return result;
	}


	string RollAlignment () {
		string alignment = null;
		int alignmentResult = roll(3,6);
			if (alignmentResult == 3 && flip == 1) {alignment = "Chaotic Evil";}
			if (alignmentResult == 3 && flip == 2) {alignment = "Chaotic Neutral";}
			if (alignmentResult >= 4 && alignmentResult <= 5) {alignment = "Lawful Evil";}
			if (alignmentResult >= 6 && alignmentResult <= 8) {alignment = "Neutral Evil";}
			if (alignmentResult >= 9 && alignmentResult <= 12) {alignment = "Neutral";}
			if (alignmentResult >= 13 && alignmentResult <= 15) {alignment = "Neutral Good";}
			if (alignmentResult >= 16 && alignmentResult <= 17 && flip == 1) {alignment = "Lawful Good";}
			if (alignmentResult >= 16 && alignmentResult <= 17 && flip == 2) {alignment = "Lawful Neutral";}
			if (alignmentResult == 18) {alignment = "Chaotic Good";}
		return alignment;
	}

	string RollOccupation () {
		string occupation = null;
			int occupationResult = roll(1,100);
			if (occupationResult >= 1 && occupationResult <= 5) {occupation = "an academic";}
			if (occupationResult >= 6 && occupationResult <= 10) {occupation = "an adventurer";}
				if (occupation == "an adventurer") {occupation += " (" + RollNPCClass () + ")";}
			if (occupationResult == 11) {occupation = "an aristocrat";}
			if (occupationResult >= 12 && occupationResult <= 26) {occupation = "an artisan or guild member";}
			if (occupationResult >= 27 && occupationResult <= 31) {occupation = "a criminal";}
			if (occupationResult >= 32 && occupationResult <= 36) {occupation = "an entertainer";}
			if (occupationResult >= 37 && occupationResult <= 38) {occupation = "an exile, hermit, or refugee";}
			if (occupationResult >= 39 && occupationResult <= 43) {occupation = "an explorer or wanderer";}
			if (occupationResult >= 44 && occupationResult <= 55) {occupation = "a farmer or herder";}
			if (occupationResult >= 56 && occupationResult <= 60) {occupation = "a hunter or trapper";}
			if (occupationResult >= 61 && occupationResult <= 75) {occupation = "a laborer";}
			if (occupationResult >= 76 && occupationResult <= 80) {occupation = "a merchant";}
			if (occupationResult >= 81 && occupationResult <= 85) {occupation = "a politican or bureaucrat";}
			if (occupationResult >= 86 && occupationResult <= 90) {occupation = "a priest";}
			if (occupationResult >= 91 && occupationResult <= 95) {occupation = "a sailor";}
			if (occupationResult >= 96 && occupationResult <= 100) {occupation = "a soldier";}
		return occupation;
	}

	string RollNPCClass () {
		string rolledClass = null;
		int rolledClassResult = roll (1, 100);
			if (rolledClassResult >= 1 && rolledClassResult <= 7) {rolledClass = "Barbarian";}
			if (rolledClassResult >= 8 && rolledClassResult <= 14) {rolledClass = "Bard";}
			if (rolledClassResult >= 15 && rolledClassResult <= 29) {rolledClass = "Cleric";}
			if (rolledClassResult >= 30 && rolledClassResult <= 36) {rolledClass = "Druid";}
			if (rolledClassResult >= 37 && rolledClassResult <= 52) {rolledClass = "Fighter";}
			if (rolledClassResult >= 53 && rolledClassResult <= 58) {rolledClass = "Monk";}
			if (rolledClassResult >= 59 && rolledClassResult <= 64) {rolledClass = "Paladin";}
			if (rolledClassResult >= 65 && rolledClassResult <= 70) {rolledClass = "Ranger";}
			if (rolledClassResult >= 71 && rolledClassResult <= 84) {rolledClass = "Rogue";}
			if (rolledClassResult >= 85 && rolledClassResult <= 89) {rolledClass = "Sorcerer";}
			if (rolledClassResult >= 90 && rolledClassResult <= 94) {rolledClass = "Warlock";}
			if (rolledClassResult >= 95 && rolledClassResult <= 100) {rolledClass = "Wizard";}
		return rolledClass;
	}

	string RollRelationship () {
		string rolledRelationship = null;
		int rolledRelationshipResult = roll (3, 4);
			if (rolledRelationshipResult >= 3 && rolledRelationshipResult <= 4) {rolledRelationship = "hostile";}
			if (rolledRelationshipResult >= 5 && rolledRelationshipResult <= 10) {rolledRelationship = "friendly";}
			if (rolledRelationshipResult >= 11 && rolledRelationshipResult <= 12) {rolledRelationship = "indifferent";}
		return rolledRelationship;
	}

	string CauseOfDeath (){
		int causeOfDeathResult = roll (1, 12);
		if (causeOfDeathResult == 1) {causeOfDeath += " (unknown causes)";}
		if (causeOfDeathResult == 2) {causeOfDeath += " (murdered)";}
		if (causeOfDeathResult == 3) {causeOfDeath += " (killed in battle)";}
		if (causeOfDeathResult == 4) {causeOfDeath += " (accident related to class/occupation)";}
		if (causeOfDeathResult == 5) {causeOfDeath += " (accident unrelated to class/occupation)";}
		if (causeOfDeathResult >= 6 && causeOfDeathResult <= 7) {causeOfDeath += " (natural causes such as disease or old age)";}
		if (causeOfDeathResult == 8) {causeOfDeath += " (apparent suicide)";}
		if (causeOfDeathResult == 9) {causeOfDeath += " (torn apart by an animal or natural disaster)";}
		if (causeOfDeathResult == 10) {causeOfDeath += " (consumed by a monster)";}
		if (causeOfDeathResult == 11) {causeOfDeath += " (executed for a crime or tortured to death)";}
		if (causeOfDeathResult == 12) {causeOfDeath += " (bizarre event; meteorite strike, slain by an angry god, slaad egg eruption, etc.)";}
		string tempCauseOfDeath = causeOfDeath;
		causeOfDeath = null;
		return tempCauseOfDeath;
	}

	string RollNPCStatus () {
		string rolledStatus = null;
		string badStatus = null;
		int rolledStatusResult = roll (3, 6);
			if (rolledStatusResult == 3) {rolledStatus = "dead";}
				if (rolledStatus == "dead") {rolledStatus += CauseOfDeath ();}
			if (rolledStatusResult >= 4 && rolledStatusResult <= 5) {rolledStatus = "missing or of unknown status";}
			int badStatusResult = roll(1,3);
						if (badStatusResult == 1) {badStatus = "injury";}
						if (badStatusResult == 2) {badStatus = "financial trouble";}
						if (badStatusResult == 3) {badStatus = "relationship difficulties";}
			if (rolledStatusResult >= 6 && rolledStatusResult <= 8) {rolledStatus = "alive, but doing poorly due to " + badStatus;}
			if (rolledStatusResult >= 9 && rolledStatusResult <= 12) {rolledStatus = "alive and well";}
			if (rolledStatusResult >= 13 && rolledStatusResult <= 15) {rolledStatus = "alive and quite successful";}
			if (rolledStatusResult >= 16 && rolledStatusResult <= 17) {rolledStatus = "alive and infamous";}
			if (rolledStatusResult == 18) {rolledStatus = "alive and famous";}
		return rolledStatus;
	}

	string RollGender () {
		int genderResult = roll (1,100);
		if (genderResult >= 1 && genderResult <= 47) {playerGender = "Male";}
		if (genderResult >= 48 && genderResult <= 94) {playerGender = "Female";}
		if (genderResult >= 95 && genderResult <= 100) {playerGender = "Nonbinary";}
		return playerGender;
	}

	string RollRace () {
		int raceResult = roll (1, 100);
		if (raceResult >= 1 && raceResult <= 37) {playerRace = "Human"; childhoodMemoriesModifier += 1;}
		if (playerRace == "Human")
		{int subrace = roll (1, 17);
			if (subrace == 1) {playerRace += " (Culture/Name Inspiration: Arabic)";}
			if (subrace == 2) {playerRace += " (Culture/Name Inspiration: Celtic)";}
			if (subrace == 3) {playerRace += " (Culture/Name Inspiration: Chinese)";}
			if (subrace == 4) {playerRace += " (Culture/Name Inspiration: Egyptian)";}
			if (subrace == 5) {playerRace += " (Culture/Name Inspiration: English)";}
			if (subrace == 6) {playerRace += " (Culture/Name Inspiration: French)";}
			if (subrace == 7) {playerRace += " (Culture/Name Inspiration: German)";}
			if (subrace == 8) {playerRace += " (Culture/Name Inspiration: Greek)";}
			if (subrace == 9) {playerRace += " (Culture/Name Inspiration: Indian)";}
			if (subrace == 10) {playerRace += " (Culture/Name Inspiration: Japanese)";}
			if (subrace == 11) {playerRace += " (Culture/Name Inspiration: Mesoamerican)";}
			if (subrace == 12) {playerRace += " (Culture/Name Inspiration: Niger-Congo)";}
			if (subrace == 13) {playerRace += " (Culture/Name Inspiration: Norse)";}
			if (subrace == 14) {playerRace += " (Culture/Name Inspiration: Polynesian)";}
			if (subrace == 15) {playerRace += " (Culture/Name Inspiration: Roman)";}
			if (subrace == 16) {playerRace += " (Culture/Name Inspiration: Slavic)";}
			if (subrace == 17) {playerRace += " (Culture/Name Inspiration: Spanish)";}
		}


		if (raceResult >= 38 && raceResult <= 47) {playerRace = "Dwarf";}
		if (playerRace == "Dwarf") 
		{int subrace = roll (1, 3);
			if (subrace == 1) {playerRace = "Hill Dwarf";}
			if (subrace == 2) {playerRace = "Mountain Dwarf";}
			if (subrace == 3) {playerRace = "Gray Dwarf";}
		}

		if (raceResult >= 48 && raceResult <= 57) {playerRace = "Elf";}
		if (playerRace == "Elf") 
		{int subrace = roll (1, 4);
			if (subrace == 1) {playerRace = "High Elf";}
			if (subrace == 2) {playerRace = "Wood Elf";}
			if (subrace == 3) {playerRace = "Dark Elf"; childhoodMemoriesModifier += 1;}
			if (subrace == 4) {playerRace = "Eladrin";}
		}

		if (raceResult >= 58 && raceResult <= 67) {playerRace = "Halfling";}
		if (playerRace == "Halfling") 
		{int subrace = roll (1, 3);
			if (subrace == 1) {playerRace = "Lightfoot Halfling"; childhoodMemoriesModifier += 1;}
			if (subrace == 2) {playerRace = "Stout Halfling";}
			if (subrace == 3) {playerRace = "Ghostwise Halfling";}
		}

		if (raceResult >= 68 && raceResult <= 72) {playerRace = "Dragonborn"; childhoodMemoriesModifier += 1;}
		if (playerRace == "Dragonborn") 
		{int subrace = roll (1, 10);
			if (subrace == 1) {playerRace += " (Black Dragon Ancestry)";}
			if (subrace == 2) {playerRace += " (Blue Dragon Ancestry)";}
			if (subrace == 3) {playerRace += " (Brass Dragon Ancestry)";}
			if (subrace == 4) {playerRace += " (Bronze Dragon Ancestry)";}
			if (subrace == 5) {playerRace += " (Copper Dragon Ancestry)";}
			if (subrace == 6) {playerRace += " (Gold Dragon Ancestry)";}
			if (subrace == 7) {playerRace += " (Green Dragon Ancestry)";}
			if (subrace == 8) {playerRace += " (Red Dragon Ancestry)";}
			if (subrace == 9) {playerRace += " (Silver Dragon Ancestry)";}
			if (subrace == 10) {playerRace += " (White Dragon Ancestry)";}
		}

		if (raceResult >= 73 && raceResult <= 77) {playerRace = "Gnome";}
		if (playerRace == "Gnome") 
		{int subrace = roll (1, 2);
			if (subrace == 1) {playerRace = "Forest Gnome";}
			if (subrace == 2) {playerRace = "Rock Gnome";}
		}

		if (raceResult >= 78 && raceResult <= 82) {playerRace = "Half-elf"; childhoodMemoriesModifier += 1;}
		if (playerRace == "Half-elf") 
		{int subrace = roll (1, 5);
			if (subrace == 1) {playerRace = "Half-elf";}
			if (subrace == 2) {playerRace = "Half-Wood elf";}
			if (subrace == 3) {playerRace = "Half-Sun/Moon elf";}
			if (subrace == 4) {playerRace = "Half-Dark elf";}
			if (subrace == 5) {playerRace = "Half-Aquatic elf";}
		}

		if (raceResult >= 83 && raceResult <= 87) {playerRace = "Half-orc";}

		if (raceResult >= 88 && raceResult <= 92) {playerRace = "Tiefling"; childhoodMemoriesModifier += 1;}
		if (playerRace == "Tiefling") 
		{int subrace = roll (1, 5);
			if (subrace == 1) {playerRace = "Tiefling";}
			if (subrace == 2) {playerRace = "Feral Tiefling";}
			if (subrace == 3) {playerRace = "Devil's Tongue Tiefling";}
			if (subrace == 4) {playerRace = "Winged Tiefling";}
			if (subrace == 5) {playerRace = "Hellfire Tiefling";}
		}

		if (raceResult == 93) {playerRace = "Aasimar"; childhoodMemoriesModifier += 1;}
		if (playerRace == "Aasimar") 
		{int subrace = roll (1, 3);
			if (subrace == 1) {playerRace = "Protector Aasimar";}
			if (subrace == 2) {playerRace = "Scourge Aasimar";}
			if (subrace == 3) {playerRace = "Fallen Aasimar";}
		}

		if (raceResult == 94) {playerRace = "Firbolg";}
		if (raceResult == 95) {playerRace = "Goliath";}
		if (raceResult == 96) {playerRace = "Kenku";}
		if (raceResult == 97) {playerRace = "Lizardfolk";}
		if (raceResult == 98) {playerRace = "Tabaxi"; childhoodMemoriesModifier += 1;}
		if (raceResult == 99) {playerRace = "Triton"; childhoodMemoriesModifier += 1;}

		if (raceResult == 100) {int monsterRaceRoll = roll (1, 6);
			if (monsterRaceRoll == 1) {playerRace = "Bugbear";}
			if (monsterRaceRoll == 2) {playerRace = "Goblin";}
			if (monsterRaceRoll == 3) {playerRace = "Hobgoblin";}
			if (monsterRaceRoll == 4) {playerRace = "Kobold";}
			if (monsterRaceRoll == 5) {playerRace = "Orc";}
			if (monsterRaceRoll == 6) {playerRace = "Yuan-ti Pureblood"; childhoodMemoriesModifier += 1;}
		}
		return playerRace;
	}

	string RollClass () {
		int classResult = roll (1, 12);
		if (classResult == 1) {playerClass = "Barbarian";}
		if (playerClass == "Barbarian") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerSubclass = " (Path of the Berserker)";}
			if (subclass == 2) {playerSubclass = " (Path of the Totem Warrior)";}
			if (subclass == 3) {playerSubclass = " (Path of the Ancestral Guardian)";}
			if (subclass == 4) {playerSubclass = " (Path of the Storm Herald)";}
			if (subclass == 5) {playerSubclass = " (Path of the Zealot)";}
			if (subclass == 6) {playerSubclass = " (Path of the Battlerager)";}
		}

		if (classResult == 2) {playerClass = "Bard"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Bard")
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (College of Lore)";}
			if (subclass == 2) {playerSubclass = " (College of Valor)";}
			if (subclass == 3) {playerSubclass = " (College of Glamour)";}
			if (subclass == 4) {playerSubclass = " (College of Swords)";}
			if (subclass == 5) {playerSubclass = " (College of Whispers)";}
		}

		if (classResult == 3) {playerClass = "Cleric";}
		if (playerClass == "Cleric") 
		{int subclass = roll (1, 11);
			if (subclass == 1) {playerSubclass = " (Knowledge Domain)";}
			if (subclass == 2) {playerSubclass = " (Life Domain)";}
			if (subclass == 3) {playerSubclass = " (Light Domain)";}
			if (subclass == 4) {playerSubclass = " (Nature Domain)";}
			if (subclass == 5) {playerSubclass = " (Tempest Domain)";}
			if (subclass == 6) {playerSubclass = " (Trickery Domain)";}
			if (subclass == 7) {playerSubclass = " (War Domain)";}
			if (subclass == 8) {playerSubclass = " (Forge Domain)";}
			if (subclass == 9) {playerSubclass = " (Grave Domain)";}
			if (subclass == 10) {playerSubclass = " (Death Domain)";}
			if (subclass == 11) {playerSubclass = " (Arcana Domain)";}
		}

		if (classResult == 4) {playerClass = "Druid";}
		if (playerClass == "Druid") 
		{int subclass = roll (1, 4);
			if (subclass == 1) {playerSubclass = " (Circle of the Land)";}
			if (subclass == 2) {playerSubclass = " (Circle of the Moon)";}
			if (subclass == 3) {playerSubclass = " (Circle of Dreams)";}
			if (subclass == 4) {playerSubclass = " (Circle of the Shepherd)";}
		}

		if (classResult == 5) {playerClass = "Fighter";}
		if (playerClass == "Fighter") 
		{int subclass = roll (1, 7);
			if (subclass == 1) {playerSubclass = " (Champion)";}
			if (subclass == 2) {playerSubclass = " (Battle Master)";}
			if (subclass == 3) {playerSubclass = " (Eldritch Knight)";}
			if (subclass == 4) {playerSubclass = " (Arcane Archer)";}
			if (subclass == 5) {playerSubclass = " (Cavalier)";}
			if (subclass == 6) {playerSubclass = " (Samurai)";}
			if (subclass == 7) {playerSubclass = " (Purple Dragon Knight)";}
		}

		if (classResult == 6) {playerClass = "Monk";}
		if (playerClass == "Monk") 
		{int subclass = roll (1, 7);
			if (subclass == 1) {playerSubclass = " (Way of the Open Hand)";}
			if (subclass == 2) {playerSubclass = " (Way of Shadow)";}
			if (subclass == 3) {playerSubclass = " (Way of the Four Elements)";}
			if (subclass == 4) {playerSubclass = " (Way of the Drunken Master)";}
			if (subclass == 5) {playerSubclass = " (Way of the Kensei)";}
			if (subclass == 6) {playerSubclass = " (Way of the Sun Soul)";}
			if (subclass == 7) {playerSubclass = " (Way of the Long Death)";}
		}

		if (classResult == 7) {playerClass = "Paladin"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Paladin") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerSubclass = " (Oath of Devotion)";}
			if (subclass == 2) {playerSubclass = " (Oath of the Ancients)";}
			if (subclass == 3) {playerSubclass = " (Oath of Vengeance)";}
			if (subclass == 4) {playerSubclass = " (Oath of Conquest)";}
			if (subclass == 5) {playerSubclass = " (Oath of Redemption)";}
			if (subclass == 6) {playerSubclass = " (Oath of the Crown)";}
		}

		if (classResult == 8) {playerClass = "Ranger";}
		if (playerClass == "Ranger") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (Hunter)";}
			if (subclass == 2) {playerSubclass = " (Beast Master)";}
			if (subclass == 3) {playerSubclass = " (Gloom Stalker)";}
			if (subclass == 4) {playerSubclass = " (Horizon Walker)";}
			if (subclass == 5) {playerSubclass = " (Monster Slayer)";}
		}

		if (classResult == 9) {playerClass = "Rogue";}
		if (playerClass == "Rogue") 
		{int subclass = roll (1, 7);
			if (subclass == 1) {playerSubclass = " (Thief)";}
			if (subclass == 2) {playerSubclass = " (Assassin)";}
			if (subclass == 3) {playerSubclass = " (Arcane Trickster)";}
			if (subclass == 4) {playerSubclass = " (Inquisitive)";}
			if (subclass == 5) {playerSubclass = " (Mastermind)";}
			if (subclass == 6) {playerSubclass = " (Scout)";}
			if (subclass == 7) {playerSubclass = " (Swashbuckler)";}
		}

		if (classResult == 10) {playerClass = "Sorcerer"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Sorcerer") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (Draconic Bloodline)";}
			if (subclass == 2) {playerSubclass = " (Wild Magic)";}
			if (subclass == 3) {playerSubclass = " (Divine Soul)";}
			if (subclass == 4) {playerSubclass = " (Shadow Magic)";}
			if (subclass == 5) {playerSubclass = " (Storm Sorcery)";}
		}

		if (classResult == 11) {playerClass = "Warlock"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Warlock") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerSubclass = " (Archfey Patron)";}
			if (subclass == 2) {playerSubclass = " (Fiend Patron)";}
			if (subclass == 3) {playerSubclass = " (Great Old One Patron)";}
			if (subclass == 4) {playerSubclass = " (Celestial Patron)";}
			if (subclass == 5) {playerSubclass = " (Hexblade Patron)";}
			if (subclass == 6) {playerSubclass = " (Undying Patron)";}
		}

		if (classResult == 12) {playerClass = "Wizard";}
		if (playerClass == "Wizard") 
		{int subclass = roll (1, 10);
			if (subclass == 1) {playerSubclass = " (School of Abjuration)";}
			if (subclass == 2) {playerSubclass = " (School of Conjuration)";}
			if (subclass == 3) {playerSubclass = " (School of Divination)";}
			if (subclass == 4) {playerSubclass = " (School of Enchantment)";}
			if (subclass == 5) {playerSubclass = " (School of Evocation)";}
			if (subclass == 6) {playerSubclass = " (School of Illusion)";}
			if (subclass == 7) {playerSubclass = " (School of Necromancy)";}
			if (subclass == 8) {playerSubclass = " (School of Transmutation)";}
			if (subclass == 9) {playerSubclass = " (War Magic)";}
			if (subclass == 10) {playerSubclass = " (Bladesinging)";}
		}
		return playerClass;
	}

	string RollClassQuirks ()
	{
		if (playerClass == "Barbarian") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "You carry a personal totem--";
			if (classQuirk1Result == 1) {classQuirk1 += "a tuft of fur from a solitary wolf that you befriended during a hunt.";}
			if (classQuirk1Result == 2) {classQuirk1 += "three eagle feathers given to you by a wise shaman, who told you they would play a role in determining your fate.";}
			if (classQuirk1Result == 3) {classQuirk1 += "a necklace made from the claws of a young cave bear that you slew singlehandedly as a child.";}
			if (classQuirk1Result == 4) {classQuirk1 += "a small leather pouch holding three stones that represent your ancestors.";}
			if (classQuirk1Result == 5) {classQuirk1 += "a few small bones from the first beast you killed, tied together with colored wool.";}
			if (classQuirk1Result == 6) {classQuirk1 += "an egg-sized stone in the shape of your spirit animal that appeared one day in your belt pouch.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You wear a notable tattoo--";
			if (classQuirk2Result == 1) {classQuirk2 += "the wings of an eagle are spread wide across your upper back.";}
			if (classQuirk2Result == 2) {classQuirk2 += "etched on the backs of your hands are the paws of a cave bear.";}
			if (classQuirk2Result == 3) {classQuirk2 += "the symbols of your clan are displayed in viny patterns along your arms.";}
			if (classQuirk2Result == 4) {classQuirk2 += "the antlers of an elk are inked across your back.";}
			if (classQuirk2Result == 5) {classQuirk2 += "images of your spirit animal are tattooed along your weapon arm and hand.";}
			if (classQuirk2Result == 6) {classQuirk2 += "the eyes of a wolf are marked on your back to help you see and ward off evil spirits.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You hold to a superstition--";
			if (classQuirk3Result == 1) {classQuirk3 += "if you disturb the bones of the dead, you inherit all the troubles that plagued them in life.";}
			if (classQuirk3Result == 2) {classQuirk3 += "never trust a wizard. They’re all devils in disguise, especially the friendly ones.";}
			if (classQuirk3Result == 3) {classQuirk3 += "dwarves have lost their spirits, and are almost like the undead. That’s why they live underground.";}
			if (classQuirk3Result == 4) {classQuirk3 += "magical things bring trouble. Never sleep with a magic object within ten feet of you.";}
			if (classQuirk3Result == 5) {classQuirk3 += "when you walk through a graveyard, be sure to wear silver, or a ghost might jump into your body.";}
			if (classQuirk3Result == 6) {classQuirk3 += "if an elf looks you in the eyes, she’s trying to read your thoughts.";}
		}

		if (playerClass == "Bard") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "Your most defining work is ";
			if (classQuirk1Result == 1) {classQuirk1 += "'The Three Flambinis,' a ribald song concerning mistaken identities and unfettered desire.";}
			if (classQuirk1Result == 2) {classQuirk1 += "'Waltz of the Myconids,' an upbeat tune that children in particular enjoy.";}
			if (classQuirk1Result == 3) {classQuirk1 += "'Asmodeus’s Golden Arse,' a dramatic poem you claim was inspired by your personal visit to Avernus.";}
			if (classQuirk1Result == 4) {classQuirk1 += "'The Pirates of Luskan,' your firsthand account of being kidnapped by sea reavers as a child.";}
			if (classQuirk1Result == 5) {classQuirk1 += "'A Hoop, Two Pigeons, and a Hell Hound,' a subtle parody of an incompetent noble.";}
			if (classQuirk1Result == 6) {classQuirk1 += "'A Fool in the Abyss,' a comedic poem about a jester’s travels among demons.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You carry a notable intstrument--";
			if (classQuirk2Result == 1) {classQuirk2 += "a masterfully crafted halfling fiddle.";}
			if (classQuirk2Result == 2) {classQuirk2 += "a mithral horn made by elves.";}
			if (classQuirk2Result == 3) {classQuirk2 += "a zither made with drow spider silk.";}
			if (classQuirk2Result == 4) {classQuirk2 += "an orcish drum.";}
			if (classQuirk2Result == 5) {classQuirk2 += "a wooden bullywug croak box.";}
			if (classQuirk2Result == 6) {classQuirk2 += "a tinker’s harp of gnomish design.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "Like most Bards, you've suffered at least one bad experience--such as ";
			if (classQuirk3Result == 1) {classQuirk3 += "the time when your comedic song, “Big Tom’s Hijinks” — which, by the way, you thought was brilliant — did not go over well with Big Tom.";}
			if (classQuirk3Result == 2) {classQuirk3 += "the matinee performance when a circus’s owlbear got loose and terrorized the crowd.";}
			if (classQuirk3Result == 3) {classQuirk3 += "when your opening song was your enthusiastic but universally hated rendition of “Song of the Froghemoth”.";}
			if (classQuirk3Result == 4) {classQuirk3 += "the first and last public performance of “Mirt, Man about Town”.";}
			if (classQuirk3Result == 5) {classQuirk3 += "the time on stage when your wig caught fire and you threw it down — which set fire to the stage.";}
			if (classQuirk3Result == 6) {classQuirk3 += "when you sat on your lute by mistake during the final stanza of “Starlight Serenade”.";}

			int classQuirk4Result = roll (1, 3);
			classQuirk4 = "Your muse (for the moment) is ";
			if (classQuirk4Result == 1) {classQuirk4 += "nature. The beauty and mystery of the natural world inspire you, and your creativity blossoms when you wander in wild places.";}
			if (classQuirk4Result == 2) {classQuirk4 += "love. You search for the essence of true love, and the full spectrum of that most precious and mysterious of emotions can be found everywhere.";}
			if (classQuirk4Result == 3) {classQuirk4 += "conflict. You strive to study this eternal aspect of life and immortalize both the epic and the dire in your stories and songs.";}
		}

		if (playerClass == "Cleric") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "You won't soon forget the temple in which you studied--";
			if (classQuirk1Result == 1) {classQuirk1 += "your temple is said to be the oldest surviving structure built to honor your god.";}
			if (classQuirk1Result == 2) {classQuirk1 += "acolytes of several like-minded deities all received instruction together in your temple.";}
			if (classQuirk1Result == 3) {classQuirk1 += "you come from a temple famed for the brewery it operates. Some say you smell like one of its ales.";}
			if (classQuirk1Result == 4) {classQuirk1 += "your temple is a fortress and a proving ground that trains warrior-priests.";}
			if (classQuirk1Result == 5) {classQuirk1 += "your temple is a peaceful, humble place, filled with vegetable gardens and simple priests.";}
			if (classQuirk1Result == 6) {classQuirk1 += "you served in a temple in the Outer Planes.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You retain a keepsake that reminds you of your vows--";
			if (classQuirk2Result == 1) {classQuirk2 += "the finger bone of a saint.";}
			if (classQuirk2Result == 2) {classQuirk2 += "a metal-bound book that tells how to hunt and destroy infernal creatures.";}
			if (classQuirk2Result == 3) {classQuirk2 += "a pig’s whistle that reminds you of your humble and beloved mentor.";}
			if (classQuirk2Result == 4) {classQuirk2 += "a braid of hair woven from the tail of a unicorn.";}
			if (classQuirk2Result == 5) {classQuirk2 += "a scroll that describes how best to rid the world of necromancers.";}
			if (classQuirk2Result == 6) {classQuirk2 += "a runestone said to be blessed by your god.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "Like all mortals, you struggle with dark desires and forbidden attractions--";
			if (classQuirk3Result == 1) {classQuirk3 += "an imp offers you counsel. You try to ignore the creature, but sometimes its advice is helpful.";}
			if (classQuirk3Result == 2) {classQuirk3 += "you believe that, in the final analysis, the gods are nothing more than ultrapowerful mortal creatures.";}
			if (classQuirk3Result == 3) {classQuirk3 += "you acknowledge the power of the gods, but you think that most events are dictated by pure chance.";}
			if (classQuirk3Result == 4) {classQuirk3 += "even though you can work divine magic, you have never truly felt the presence of a divine essence within yourself.";}
			if (classQuirk3Result == 5) {classQuirk3 += "you are plagued by nightmares that you believe are sent by your god as punishment for some unknown transgression.";}
			if (classQuirk3Result == 6) {classQuirk3 += "in times of despair, you feel that you are but a plaything of the gods, and you resent their remoteness.";}
		}

		if (playerClass == "Druid") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "You always carry a treasured item on your person--";
			if (classQuirk1Result == 1) {classQuirk1 += "a twig from the meeting tree that stands in the center of your village.";}
			if (classQuirk1Result == 2) {classQuirk1 += "a vial of water from the source of a sacred river.";}
			if (classQuirk1Result == 3) {classQuirk1 += "special herbs tied together in a bundle.";}
			if (classQuirk1Result == 4) {classQuirk1 += "a small bronze bowl engraved with animal images.";}
			if (classQuirk1Result == 5) {classQuirk1 += "a rattle made from a dried gourd and holly berries.";}
			if (classQuirk1Result == 6) {classQuirk1 += "a miniature golden sickle handed down to you by your mentor.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You have chosen a guiding aspect, which has qualities you seek to emulate--";
			if (classQuirk2Result == 1) {classQuirk2 += "yew trees remind you of renewing your mind and spirit, letting the old die and the new spring forth.";}
			if (classQuirk2Result == 2) {classQuirk2 += "oak trees represent strength and vitality. Meditating under an oak fills your body and mind with resolve and fortitude.";}
			if (classQuirk2Result == 3) {classQuirk2 += "the river’s endless flow reminds you of the great span of the world. You seek to act with the long-term interests of nature in mind.";}
			if (classQuirk2Result == 4) {classQuirk2 += "the sea is a constant, churning cauldron of power and chaos. It reminds you that accepting change is necessary to sustain yourself in the world.";}
			if (classQuirk2Result == 5) {classQuirk2 += "the birds in the sky are evidence that even the smallest creatures can survive if they remain above the fray.";}
			if (classQuirk2Result == 6) {classQuirk2 += "as demonstrated by the actions of the wolf, an individual’s strength is nothing compared to the power of the pack.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You learned the basics of druidic teachings and magic from a mentor; ";
			if (classQuirk3Result == 1) {classQuirk3 += "your mentor was a wise treant who taught you to think in terms of years and decades rather than days or months.";}
			if (classQuirk3Result == 2) {classQuirk3 += "you were tutored by a dryad who watched over a slumbering portal to the Abyss. During your training, you were tasked with watching for hidden threats to the world.";}
			if (classQuirk3Result == 3) {classQuirk3 += "your tutor always interacted with you in the form of a falcon. You never saw the tutor’s humanoid form.";}
			if (classQuirk3Result == 4) {classQuirk3 += "you were one of several youngsters who were mentored by an old druid, until one of your fellow pupils betrayed your group and killed your master.";}
			if (classQuirk3Result == 5) {classQuirk3 += "your mentor has appeared to you only in visions. You have yet to meet this person, and you are not sure such a person exists in mortal form.";}
			if (classQuirk3Result == 6) {classQuirk3 += "your mentor was a werebear who taught you to treat all living things with equal regard.";}
		}

		if (playerClass == "Fighter") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "You bear a heraldic sign on your gear--";
			if (classQuirk1Result == 1) {classQuirk1 += "a rampant golden dragon on a green field, representing valor and a quest for wealth.";}
			if (classQuirk1Result == 2) {classQuirk1 += "the fist of a storm giant clutching lightning before a storm cloud, symbolizing wrath and power.";}
			if (classQuirk1Result == 3) {classQuirk1 += "crossed greatswords in front of a castle gate, signifying the defense of a city or kingdom.";}
			if (classQuirk1Result == 4) {classQuirk1 += "a skull with a dagger through it, representing the doom you bring to your enemies.";}
			if (classQuirk1Result == 5) {classQuirk1 += "a phoenix in a ring of fire, an expression of an indomitable spirit.";}
			if (classQuirk1Result == 6) {classQuirk1 += "three drops of blood beneath a horizontal sword blade on a black background, symbolizing three foes you have sworn to kill.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You learned some of your martial skills from an instructor, who was ";
			if (classQuirk2Result == 1) {classQuirk2 += "a gladiator. Your instructor was a slave who fought for freedom in the arena, or one who willingly chose the gladiator’s life to earn money and fame.";}
			if (classQuirk2Result == 2) {classQuirk2 += "military. Your trainer served with a group of soldiers and knows much about working as a team.";}
			if (classQuirk2Result == 3) {classQuirk2 += "part of the city watch. Crowd control and peacekeeping are your instructor’s specialties.";}
			if (classQuirk2Result == 4) {classQuirk2 += "a tribal warrior. Your instructor grew up in a tribe, where fighting for one’s life was practically an everyday occurrence.";}
			if (classQuirk2Result == 5) {classQuirk2 += "a street fighter. Your trainer excels at urban combat, combining close-quarters work with silence and efficiency.";}
			if (classQuirk2Result == 6) {classQuirk2 += "a weapon master. Your mentor helped you to become one with your chosen weapon, by imparting highly specialized knowledge of how to wield it most effectively.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "Like all of the greatest warriors, you fight with a signature style. Your style is ";
			if (classQuirk3Result == 1) {classQuirk3 += "elegant; you move with precise grace and total control, never using more energy than you need.";}
			if (classQuirk3Result == 2) {classQuirk3 += "brutal; your attacks rain down like hammer blows, meant to splinter bone or send blood flying.";}
			if (classQuirk3Result == 3) {classQuirk3 += "cunning; you dart in to attack at just the right moment and use small-scale tactics to tilt the odds in your favor.";}
			if (classQuirk3Result == 4) {classQuirk3 += "effortless; you rarely perspire or display anything other than a stoic expression in battle. ";}
			if (classQuirk3Result == 5) {classQuirk3 += "energetic; you sing and laugh during combat as your spirit soars. You are happiest when you have a foe in front of you and a weapon in hand.";}
			if (classQuirk3Result == 6) {classQuirk3 += "sinister; you scowl and sneer while fighting, and you enjoy mocking your foes as you defeat them.";}
		}

		if (playerClass == "Monk") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "You spent time in a monastery, which ";
			if (classQuirk1Result == 1) {classQuirk1 += "is carved out of a mountainside, where it looms over a treacherous pass.";}
			if (classQuirk1Result == 2) {classQuirk1 += "is high in the branches of an immense tree in the Feywild.";}
			if (classQuirk1Result == 3) {classQuirk1 += "was founded long ago by a cloud giant and is inside a cloud castle that can be reached only by flying.";}
			if (classQuirk1Result == 4) {classQuirk1 += "is built beside a volcanic system of hot springs, geysers, and sulfur pools. You regularly received visits from azer traders.";}
			if (classQuirk1Result == 5) {classQuirk1 += "was founded by gnomes and is an underground labyrinth of tunnels and rooms.";}
			if (classQuirk1Result == 6) {classQuirk1 += "was carved from an iceberg in the frozen reaches of the world.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "Your monastic order gives special regard to ";
			if (classQuirk2Result == 1) {classQuirk2 += "Monkey. Quick reflexes and the ability to travel through the treetops are two of the reasons why your order admires the monkey.";}
			if (classQuirk2Result == 2) {classQuirk2 += "Dragon Turtle. The monks of your seaside monastery venerate the dragon turtle, reciting ancient prayers and offering garlands of flowers to honor this living spirit of the sea.";}
			if (classQuirk2Result == 3) {classQuirk2 += "Ki-rin. Your monastery sees its main purpose as watching over and protecting the land in the manner of the ki-rin.";}
			if (classQuirk2Result == 4) {classQuirk2 += "Owlbear. The monks of your monastery revere a family of owlbears and have coexisted with them for generations.";}
			if (classQuirk2Result == 5) {classQuirk2 += "Hydra. Your order singles out the hydra for its ability to unleash several attacks simultaneously.";}
			if (classQuirk2Result == 6) {classQuirk2 += "Dragon. A dragon once laired within your monastery. Its influence remains long after its departure.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You studied under a master of your order; ";
			if (classQuirk3Result == 1) {classQuirk3 += "your master was a tyrant whom you had to defeat in single combat to complete your instruction.";}
			if (classQuirk3Result == 2) {classQuirk3 += "your master was kindly and taught you to pursue the cause of peace.";}
			if (classQuirk3Result == 3) {classQuirk3 += "your master was merciless in pushing you to your limits. You nearly lost an eye during one especially brutal practice session.";}
			if (classQuirk3Result == 4) {classQuirk3 += "your master seemed goodhearted while tutoring you, but betrayed your monastery in the end.";}
			if (classQuirk3Result == 5) {classQuirk3 += "your master was cold and distant. You suspect that the two of you might be related.";}
			if (classQuirk3Result == 6) {classQuirk3 += "your master was kind and generous, never critical of your progress. Nevertheless, you feel you never fully lived up to the expectations placed on you.";}
		}

		if (playerClass == "Paladin") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "Your oath gives you purpose, but you strive toward a personal goal--";
			if (classQuirk1Result == 1) {classQuirk1 += "peace. You fight so that future generations will not have to.";}
			if (classQuirk1Result == 2) {classQuirk1 += "revenge. Your oath is the vehicle through which you will right an ancient wrong.";}
			if (classQuirk1Result == 3) {classQuirk1 += "duty. You will live up to what you have sworn to do, or die trying.";}
			if (classQuirk1Result == 4) {classQuirk1 += "leadership. You will win a great battle that bards will sing about, and in so doing, you will become an example to inspire others.";}
			if (classQuirk1Result == 5) {classQuirk1 += "faith. You know your path is righteous, or else the gods would not have set you upon it.";}
			if (classQuirk1Result == 6) {classQuirk1 += "glory. You will lead the world into a grand new era, one that will be branded with your name.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "Upon your banner, flag, or clothing, you bear the symbol of ";
			if (classQuirk2Result == 1) {classQuirk2 += "a dragon, emblematic of your nobility in peace and your ferocity in combat.";}
			if (classQuirk2Result == 2) {classQuirk2 += "a clenched fist, because you are always ready to fight for your beliefs.";}
			if (classQuirk2Result == 3) {classQuirk2 += "an upraised open hand, indicating your preference for diplomacy over combat.";}
			if (classQuirk2Result == 4) {classQuirk2 += "a red heart, showing the world your commitment to justice.";}
			if (classQuirk2Result == 5) {classQuirk2 += "a black heart, signifying that emotions such as pity do not sway your dedication to your oath.";}
			if (classQuirk2Result == 6) {classQuirk2 += "an unblinking eye, meaning that you are ever alert to all threats against your cause.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You remain vigilant against the actions of your nemesis, ";
			if (classQuirk3Result == 1) {classQuirk3 += "a mighty orc war chief who threatens to overrun and destroy everything you hold sacred.";}
			if (classQuirk3Result == 2) {classQuirk3 += "a fiend or a celestial, the agent of a power of the Outer Planes, who has been charged with corrupting or redeeming you, as appropriate.";}
			if (classQuirk3Result == 3) {classQuirk3 += "a dragon whose servants dog your steps.";}
			if (classQuirk3Result == 4) {classQuirk3 += "a high priest who sees you as a misguided fool and wants you to abandon your religion.";}
			if (classQuirk3Result == 5) {classQuirk3 += "a rival paladin who trained with you but became an oath-breaker and holds you responsible.";}
			if (classQuirk3Result == 6) {classQuirk3 += "a vampire who has sworn revenge against all paladins after being defeated by one.";}

			int classQuirk4Result = roll (1, 6);
			classQuirk4 = "Although you are devoted, you are mortal, and thusly flawed; you struggle with your ";
			if (classQuirk4Result == 1) {classQuirk4 += "fury. When your anger is roused, you have trouble thinking straight, and you fear you might do something you’ll regret.";}
			if (classQuirk4Result == 2) {classQuirk4 += "pride. Your deeds are noteworthy, and no one takes note of them more often than you.";}
			if (classQuirk4Result == 3) {classQuirk4 += "lust. You can’t resist an attractive face and a pleasant smile.";}
			if (classQuirk4Result == 4) {classQuirk4 += "envy. You are mindful of what some famous folk have accomplished, and you feel inadequate when your deeds don’t compare to theirs.";}
			if (classQuirk4Result == 5) {classQuirk4 += "despair. You consider the great strength of the enemies you must defeat, and at times you see no way to achieve final victory.";}
			if (classQuirk4Result == 6) {classQuirk4 += "greed. Regardless of how much glory and treasure you amass, it’s never enough for you.";}
		}

		if (playerClass == "Ranger") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "Your view of civilization is colored by your time in the wilds; ";
			if (classQuirk1Result == 1) {classQuirk1 += "Towns and cities are the best places for those who can’t survive on their own.";}
			if (classQuirk1Result == 2) {classQuirk1 += "The advancement of civilization is the best way to thwart chaos, but its reach must be monitored.";}
			if (classQuirk1Result == 3) {classQuirk1 += "Towns and cities are a necessary evil, but once the wilderness is purged of supernatural threats, we will need them no more.";}
			if (classQuirk1Result == 4) {classQuirk1 += "Walls are for cowards, who huddle behind them while others do the work of making the world safe.";}
			if (classQuirk1Result == 5) {classQuirk1 += "Visiting a town is not unpleasant, but after a few days you feel the irresistible call to return to the wild.";}
			if (classQuirk1Result == 6) {classQuirk1 += "Cities breed weakness by isolating folk from the harsh lessons of the wild.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "Your time in your homeland honed your bushcraft--";
			if (classQuirk2Result == 1) {classQuirk2 += "you patrolled an ancient forest, darkened and corrupted by several crossings to the Shadowfell.";}
			if (classQuirk2Result == 2) {classQuirk2 += "as part of a group of nomads, you acquired the skills for surviving in the desert.";}
			if (classQuirk2Result == 3) {classQuirk2 += "your early life in the Underdark prepared you for the challenges of combating its denizens.";}
			if (classQuirk2Result == 4) {classQuirk2 += "you dwelled on the edge of a swamp, in an area imperiled by land creatures as well as aquatic ones.";}
			if (classQuirk2Result == 5) {classQuirk2 += "because you grew up among the peaks, finding the best path through the mountains is second nature to you.";}
			if (classQuirk2Result == 6) {classQuirk2 += "you wandered the far north, learning how to protect yourself and prosper in a realm overrun by ice.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You often dwell on your sworn enemy; ";
			if (classQuirk3Result == 1) {classQuirk3 += "you seek revenge on nature’s behalf for the great transgressions your foe has committed.";}
			if (classQuirk3Result == 2) {classQuirk3 += "your forebears or predecessors fought these creatures, and so shall you.";}
			if (classQuirk3Result == 3) {classQuirk3 += "you bear no enmity toward your foe. You stalk such creatures as a hunter tracks down a wild animal.";}
			if (classQuirk3Result == 4) {classQuirk3 += "you find your foe fascinating, and you collect books of tales and history concerning it.";}
			if (classQuirk3Result == 5) {classQuirk3 += "you collect tokens of your fallen enemies to remind you of each kill.";}
			if (classQuirk3Result == 6) {classQuirk3 += "you respect your chosen enemy, and you see your battles as a test of respective skills.";}
		}

		if (playerClass == "Rogue") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "Even though it might mean trouble for you and your companions, you just can't resist ";
			if (classQuirk1Result == 1) {classQuirk1 += "large gems.";}
			if (classQuirk1Result == 2) {classQuirk1 += "a smile from a pretty face.";}
			if (classQuirk1Result == 3) {classQuirk1 += "a new ring for your finger.";}
			if (classQuirk1Result == 4) {classQuirk1 += "the chance to deflate someone’s ego.";}
			if (classQuirk1Result == 5) {classQuirk1 += "the finest food and drink.";}
			if (classQuirk1Result == 6) {classQuirk1 += "adding to your collection of exotic coins.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "You have your share of foes bested (or best avoided), but one adversary stands out: ";
			if (classQuirk2Result == 1) {classQuirk2 += "the pirate captain on whose ship you once served; what you call moving on, the captain calls mutiny.";}
			if (classQuirk2Result == 2) {classQuirk2 += "a master spy to whom you unwittingly fed bad information, which led to the assassination of the wrong target.";}
			if (classQuirk2Result == 3) {classQuirk2 += "the master of the local thieves’ guild, who wants you to join the organization or leave town.";}
			if (classQuirk2Result == 4) {classQuirk2 += "an art collector who uses illegal means to acquire masterpieces.";}
			if (classQuirk2Result == 5) {classQuirk2 += "a fence who uses you as a messenger to set up illicit meetings.";}
			if (classQuirk2Result == 6) {classQuirk2 += "the proprietor of an illegal pit fighting arena where you once took bets.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You have benefited from the largess of others--";
			if (classQuirk3Result == 1) {classQuirk3 += "a smuggler kept you from getting caught but lost a valuable shipment in doing so. Now you owe that person an equally valuable favor.";}
			if (classQuirk3Result == 2) {classQuirk3 += "the Beggar King has hidden you from your pursuers many times, in return for future considerations.";}
			if (classQuirk3Result == 3) {classQuirk3 += "a magistrate once kept you out of jail in return for information on a powerful crime lord.";}
			if (classQuirk3Result == 4) {classQuirk3 += "your parents used their savings to bail you out of trouble in your younger days and are now destitute.";}
			if (classQuirk3Result == 5) {classQuirk3 += "a dragon didn’t eat you when it had a chance, and in return you promised to set aside choice pieces of treasure for it.";}
			if (classQuirk3Result == 6) {classQuirk3 += "a druid once helped you out of a tight spot; now any random animal you see could be that benefactor, perhaps come to claim a return favor.";}
		}

		if (playerClass == "Sorcerer") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "The source of your arcane power is potent--";
			if (classQuirk1Result == 1) {classQuirk1 += "your power arises from your family’s bloodline. You are related to some powerful creature, or you inherited a blessing or a curse.";}
			if (classQuirk1Result == 2) {classQuirk1 += "you are the reincarnation of a being from another plane of existence.";}
			if (classQuirk1Result == 3) {classQuirk1 += "a powerful entity entered the world. Its magic changed you.";}
			if (classQuirk1Result == 4) {classQuirk1 += "your birth was prophesied in an ancient text, and you are foretold to use your power for terrible ends.";}
			if (classQuirk1Result == 5) {classQuirk1 += "you are the product of generations of careful, selective breeding.";}
			if (classQuirk1Result == 6) {classQuirk1 += "you were made in a vat by an alchemist.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "The world could not ignore the manifestation of your magic; ";
			if (classQuirk2Result == 1) {classQuirk2 += "your powers are seen as a great blessing by those around you, and you are expected to use them in service to your community.";}
			if (classQuirk2Result == 2) {classQuirk2 += "your powers caused destruction and even a death when they became evident, and you were treated as a criminal.";}
			if (classQuirk2Result == 3) {classQuirk2 += "your neighbors hate and fear your power, causing them to shun you.";}
			if (classQuirk2Result == 4) {classQuirk2 += "you came to the attention of a sinister cult that plans on exploiting your abilities.";}
			if (classQuirk2Result == 5) {classQuirk2 += "people around you believe that your powers are a curse levied on your family for a past transgression.";}
			if (classQuirk2Result == 6) {classQuirk2 += "your powers are believed to be tied to an ancient line of mad kings that supposedly ended in a bloody revolt over a century ago.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "Your body bears a curious supernatural mark: ";
			if (classQuirk3Result == 1) {classQuirk3 += "your eyes are an unusual color, such as red.";}
			if (classQuirk3Result == 2) {classQuirk3 += "you have an extra toe on one foot.";}
			if (classQuirk3Result == 3) {classQuirk3 += "one of your ears is noticeably larger than the other.";}
			if (classQuirk3Result == 4) {classQuirk3 += "your hair grows at a prodigious rate.";}
			if (classQuirk3Result == 5) {classQuirk3 += "you wrinkle your nose repeatedly while you are chewing.";}
			if (classQuirk3Result == 6) {classQuirk3 += "a red splotch appears on your neck once a day, then vanishes after an hour.";}

			int classQuirk4Result = roll (1, 6);
			classQuirk4 = "Your magic is always accompanied by a telltale sign of sorcery--";
			if (classQuirk4Result == 1) {classQuirk4 += "you deliver the verbal components of your spells in the booming voice of a titan.";}
			if (classQuirk4Result == 2) {classQuirk4 += "for a moment after you cast a spell, the area around you grows dark and gloomy.";}
			if (classQuirk4Result == 3) {classQuirk4 += "you sweat profusely while casting a spell and for a few seconds thereafter.";}
			if (classQuirk4Result == 4) {classQuirk4 += "your hair and garments are briefly buffeted about, as if by a breeze, whenever you call forth a spell.";}
			if (classQuirk4Result == 5) {classQuirk4 += "if you are standing when you cast a spell, you rise six inches into the air and gently float back down.";}
			if (classQuirk4Result == 6) {classQuirk4 += "illusory blue flames wreathe your head as you begin your casting, then abruptly disappear.";}
		}

		if (playerClass == "Warlock") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "No relationship between a warlock and their patron is simple; ";
			if (classQuirk1Result == 1) {classQuirk1 += "your patron has guided and helped your family for generations and is kindly toward you.";}
			if (classQuirk1Result == 2) {classQuirk1 += "each interaction with your capricious patron is a surprise, whether pleasant or painful.";}
			if (classQuirk1Result == 3) {classQuirk1 += "your patron is the spirit of a long-dead hero who sees your pact as a way for it to continue to influence the world.";}
			if (classQuirk1Result == 4) {classQuirk1 += "your patron is a strict disciplinarian but treats you with a measure of respect.";}
			if (classQuirk1Result == 5) {classQuirk1 += "your patron tricked you into a pact and treats you as a slave.";}
			if (classQuirk1Result == 6) {classQuirk1 += "you are mostly left to your own devices with no interference from your patron. Sometimes you dread the demands it will make when it does appear.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "Your pact carries special terms--";
			if (classQuirk2Result == 1) {classQuirk2 += "when directed, you must take immediate action against a specific enemy of your patron.";}
			if (classQuirk2Result == 2) {classQuirk2 += "your pact tests your willpower; you are required to abstain from alcohol and other intoxicants.";}
			if (classQuirk2Result == 3) {classQuirk2 += "at least once a day, you must inscribe or carve your patron’s name or symbol on the wall of a building.";}
			if (classQuirk2Result == 4) {classQuirk2 += "you must occasionally conduct bizarre rituals to maintain your pact.";}
			if (classQuirk2Result == 5) {classQuirk2 += "you can never wear the same outfit twice, since your patron finds such predictability to be boring.";}
			if (classQuirk2Result == 6) {classQuirk2 += "when you use an eldritch invocation, you must speak your patron’s name aloud or risk incurring its displeasure.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "You bear the mark of your patron: ";
			if (classQuirk3Result == 1) {classQuirk3 += "one of your eyes looks the same as one of your patron’s eyes.";}
			if (classQuirk3Result == 2) {classQuirk3 += "each time you wake up, the small blemish on your face appears in a different place.";}
			if (classQuirk3Result == 3) {classQuirk3 += "you display outward symptoms of a disease but suffer no ill effects from it.";}
			if (classQuirk3Result == 4) {classQuirk3 += "your tongue is an unnatural color.";}
			if (classQuirk3Result == 5) {classQuirk3 += "you have a vestigial tail.";}
			if (classQuirk3Result == 6) {classQuirk3 += "your nose glows in the dark.";}
		}

		if (playerClass == "Wizard") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "Your treasured spellbook carries your personal flair--it is ";
			if (classQuirk1Result == 1) {classQuirk1 += "a tome with pages that are thin sheets of metal, spells etched into them with acid.";}
			if (classQuirk1Result == 2) {classQuirk1 += "long straps of leather on which spells are written, wrapped around a staff for ease of transport.";}
			if (classQuirk1Result == 3) {classQuirk1 += "a battered tome filled with pictographs that only you can understand.";}
			if (classQuirk1Result == 4) {classQuirk1 += "small stones inscribed with spells and kept in a cloth bag.";}
			if (classQuirk1Result == 5) {classQuirk1 += "a scorched book, ravaged by dragon fire, with the script of your spells barely visible on its pages.";}
			if (classQuirk1Result == 6) {classQuirk1 += "a tome full of black pages whose writing is visible only in dim light or darkness.";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "Your magical scholarship is the means to enact your ambitions; ";
			if (classQuirk2Result == 1) {classQuirk2 += "you will prove that the gods aren’t as powerful as folk believe.";}
			if (classQuirk2Result == 2) {classQuirk2 += "immortality is the end goal of your studies.";}
			if (classQuirk2Result == 3) {classQuirk2 += "if you can fully understand magic, you can unlock its use for all and usher in an era of equality.";}
			if (classQuirk2Result == 4) {classQuirk2 += "magic is a dangerous tool. You use it to protect what you treasure.";}
			if (classQuirk2Result == 5) {classQuirk2 += "arcane power must be taken away from those who would abuse it.";}
			if (classQuirk2Result == 6) {classQuirk2 += "you will become the greatest wizard the world has seen in generations.";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "Endless hours of research have taken somewhat of a toll on your social skills; ";
			if (classQuirk3Result == 1) {classQuirk3 += "you have the habit of tapping your foot incessantly, which often annoys those around you.";}
			if (classQuirk3Result == 2) {classQuirk3 += "your memory is quite good, but you have no trouble pretending to be absentminded when it suits your purposes.";}
			if (classQuirk3Result == 3) {classQuirk3 += "you never enter a room without looking to see what’s hanging from the ceiling.";}
			if (classQuirk3Result == 4) {classQuirk3 += "your most prized possession is a dead worm that you keep inside a potion vial.";}
			if (classQuirk3Result == 5) {classQuirk3 += "when you want people to leave you alone, you start talking to yourself. That usually does the trick.";}
			if (classQuirk3Result == 6) {classQuirk3 += "your fashion sense and grooming, or more accurately lack thereof, sometimes cause others to assume you are a beggar.";}
		}

		string output = classQuirk1 + " " + classQuirk2 + " " + classQuirk3 + " " + classQuirk4;
		return output;
	}

	string RollBackground () {

		int backgroundResult = roll (1, 13);

		int playerPersonalityResult = roll(1,8); 
		int playerIdealResult = roll(1,6); 
		int playerBondResult = roll(1,6); 
		int playerFlawResult = roll(1,6); 

		if (backgroundResult == 1) {playerBackground = "an Acolyte";}
		if (playerBackground == "an Acolyte") {playerBackgroundSpecial = null;}

		if (playerPersonalityResult == 1 && playerBackground == "an Acolyte") {playerPersonality = "You idolize a particular hero of my faith, and constantly refer to that person's deeds and example.";}
		if (playerPersonalityResult == 2 && playerBackground == "an Acolyte") {playerPersonality = "You can find common ground between the fiercest enemies, empathizing wilh them and always working toward peace.";}
		if (playerPersonalityResult == 3 && playerBackground == "an Acolyte") {playerPersonality = "You see omens in every event and action. The gods try to speak to us, we just need lo listen.";}
		if (playerPersonalityResult == 4 && playerBackground == "an Acolyte") {playerPersonality = "Nothing can shake my optimistic attitude.";}
		if (playerPersonalityResult == 5 && playerBackground == "an Acolyte") {playerPersonality = "You quote (or misquote) sacred texts and proverbs in almost every situation.";}
		if (playerPersonalityResult == 6 && playerBackground == "an Acolyte") {playerPersonality = "You are tolerant (or intolerant) of other faiths and respect (or condemn) the worship of other gods.";}
		if (playerPersonalityResult == 7 && playerBackground == "an Acolyte") {playerPersonality = "You've enjoyed fine food, drink, and high society among your temple's elite. Rough living grates on you.";}
		if (playerPersonalityResult == 8 && playerBackground == "an Acolyte") {playerPersonality = "You've spent so long in the temple that you have liltle practical experience dealing with people in the outside world.";}

		if (playerIdealResult == 1 && playerBackground == "an Acolyte") {playerIdeal = "Tradition. The ancient traditions of worship and sacrifice must be preserved and upheld. (Lawful)";}
		if (playerIdealResult == 2 && playerBackground == "an Acolyte") {playerIdeal = "Charity. You always try to help those in need, no matter what the personal cost. (Good)";}
		if (playerIdealResult == 3 && playerBackground == "an Acolyte") {playerIdeal = "Change. We must help bring about the changes the gods are constantly working in the world. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "an Acolyte") {playerIdeal = "Power. You hope to one day rise to the top of your faith's religious hierarchy. (Lawful)";}
		if (playerIdealResult == 5 && playerBackground == "an Acolyte") {playerIdeal = "Faith. You trust that your deity will guide your actions. You have faith that if you work hard, things will go well. (Lawful)";}
		if (playerIdealResult == 6 && playerBackground == "an Acolyte") {playerIdeal = "Aspiration. You seek to prove yourself worthy of your god's favor by matching your actions against his or her teachings. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "an Acolyte") {playerBond = "You would die to recover an ancient relic of your faith that was lost long ago.";}
		if (playerBondResult == 2 && playerBackground == "an Acolyte") {playerBond = "You will someday get revenge on the corrupt temple hierarchy who branded you a heretic.";}
		if (playerBondResult == 3 && playerBackground == "an Acolyte") {playerBond = "You owe your life to the priest who took you in when your parents died.";}
		if (playerBondResult == 4 && playerBackground == "an Acolyte") {playerBond = "Everything you do is for the common people.";}
		if (playerBondResult == 5 && playerBackground == "an Acolyte") {playerBond = "You will do anything to protect the temple where you served.";}
		if (playerBondResult == 6 && playerBackground == "an Acolyte") {playerBond = "You seek to preserve a sacred text that your enemies consider heretical and seek to destroy.";}

		if (playerFlawResult == 1 && playerBackground == "an Acolyte") {playerFlaw = "You judge others harshly, and yourself even more severely. ";}
		if (playerFlawResult == 2 && playerBackground == "an Acolyte") {playerFlaw = "You put too much trust in those who wield power within your temple's hierarchy.";}
		if (playerFlawResult == 3 && playerBackground == "an Acolyte") {playerFlaw = "Your piety sometimes leads you to blindly trust those that profess faith in your god.";}
		if (playerFlawResult == 4 && playerBackground == "an Acolyte") {playerFlaw = "You are inflexible in your thinking.";}
		if (playerFlawResult == 5 && playerBackground == "an Acolyte") {playerFlaw = "You are suspicious of strangers and expect the worst of them.";}
		if (playerFlawResult == 6 && playerBackground == "an Acolyte") {playerFlaw = "Once you pick a goal, you become obsessed with it to the detriment of everything else in your life.";}

		if (backgroundResult == 2) {playerBackground = "a Charlatan";}
		if (playerBackground == "a Charlatan") {playerBackgroundSpecialResult = roll(1,6); playerBackgroundSpecial = "Favorite Scheme: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You cheat at games of chance.";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You shave coins or forge documents.";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You insinuate yourself into people's lives to prey on their weakness and secure their fortunes.";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You put on new identities like clothes.";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You run sleight-of-hand cons on street corners.";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Charlatan") {playerBackgroundSpecial += "You convince people that worthless junk is worth their hard-earned money.";}

		if (playerPersonalityResult == 1 && playerBackground == "a Charlatan") {playerPersonality = "You fall in and out of love easily, and are always pursuing someone.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Charlatan") {playerPersonality = "You have a joke for every occasion, especially occasions where humor is inappropriate.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Charlatan") {playerPersonality = "Flattery is your preferred trick for getting what you want.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Charlatan") {playerPersonality = "You're a born gambler who can't resist taking a risk for a potential payoff.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Charlatan") {playerPersonality = "You lie about almost everything, even when there's no good reason to.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Charlatan") {playerPersonality = "Sarcasm and insults are your weapons of choice.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Charlatan") {playerPersonality = "You keep multiple holy symbols on you and invoke whatever deity might come in useful at any given moment.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Charlatan") {playerPersonality = "You pocket anything you see that might have some value.";}

		if (playerIdealResult == 1 && playerBackground == "a Charlatan") {playerIdeal = "Independence. You are a free spirit--no one tells you what to do. (Chaotic)";}
		if (playerIdealResult == 2 && playerBackground == "a Charlatan") {playerIdeal = "Fairness. You never target people who can't afford to lose a few coins. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Charlatan") {playerIdeal = "Charity. You distribute the money you acquire to the people who really need it. (Good)";}
		if (playerIdealResult == 4 && playerBackground == "a Charlatan") {playerIdeal = "Creativity. You never run the same con twice. (Chaotic)";}
		if (playerIdealResult == 5 && playerBackground == "a Charlatan") {playerIdeal = "Friendship. Material goods come and go. Bonds of friendship last forever. (Good)";}
		if (playerIdealResult == 6 && playerBackground == "a Charlatan") {playerIdeal = "Aspiration. You're determined to make something of yourself. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Charlatan") {playerBond = "You fleeced the wrong person and must work to ensure that this individual never crosses paths with you or those you care about.";}
		if (playerBondResult == 2 && playerBackground == "a Charlatan") {playerBond = "You owe everything to your mentor--a horrible person who's probably rotting in jail somewhere.";}
		if (playerBondResult == 3 && playerBackground == "a Charlatan") {playerBond = "Somewhere out there, you have a child who doesn't know you. You're making the world better for him or her.";}
		if (playerBondResult == 4 && playerBackground == "a Charlatan") {playerBond = "You come from a noble family, and one day you'll reclaim your lands and title from those who stole them from you.";}
		if (playerBondResult == 5 && playerBackground == "a Charlatan") {playerBond = "A powerful person killed someone you love. Some day soon, you'll have your revenge.";}
		if (playerBondResult == 6 && playerBackground == "a Charlatan") {playerBond = "You swindled and ruined a person who didn't deserve it. You seek to atone for your misdeeds but might never be able to forgive yourself.";}

		if (playerFlawResult == 1 && playerBackground == "a Charlatan") {playerFlaw = "You can't resist a pretty face.";}
		if (playerFlawResult == 2 && playerBackground == "a Charlatan") {playerFlaw = "You're always in debt. You spend your ill-gotten gains on decadent luxuries faster than you can bring them in.";}
		if (playerFlawResult == 3 && playerBackground == "a Charlatan") {playerFlaw = "You're convinced that no one could ever fool you the way you fool others.";}
		if (playerFlawResult == 4 && playerBackground == "a Charlatan") {playerFlaw = "You're too greedy for your own good. You can't resist taking a risk if there's money involved.";}
		if (playerFlawResult == 5 && playerBackground == "a Charlatan") {playerFlaw = "You can't resist swindling people who are more powerful than you.";}
		if (playerFlawResult == 6 && playerBackground == "a Charlatan") {playerFlaw = "You hate to admit it and will hate yourself for it, but you'll run and preserve your own hide if the going gets tough.";}

		if (backgroundResult == 3 && flip == 1) {playerBackground = "a Spy";}
		if (backgroundResult == 3 && flip == 2) {playerBackground = "a Criminal";}
		if (playerBackground == "a Criminal") {playerBackgroundSpecialResult = roll(1,8); playerBackgroundSpecial = "Criminal Specialty: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Blackmailer";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Burglar";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Enforcer";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Fence";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Highway robber";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Hired killer";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Pickpocket";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Criminal") {playerBackgroundSpecial += "Smuggler";}

		if (playerPersonalityResult == 1 && playerBackground == "a Criminal" || playerPersonalityResult == 1 && playerBackground == "a Spy") {playerPersonality = "You always have a plan for what to do when things go wrong.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Criminal" || playerPersonalityResult == 2 && playerBackground == "a Spy") {playerPersonality = "You are always calm, no matter the situation. You never raise your voice or let your emotions control you.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Criminal" || playerPersonalityResult == 3 && playerBackground == "a Spy") {playerPersonality = "The first thing you do in a new place is note the locations of everything valuable--or where such things could be hidden.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Criminal" || playerPersonalityResult == 4 && playerBackground == "a Spy") {playerPersonality = "You would rather make a new friend than a new enemy.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Criminal" || playerPersonalityResult == 5 && playerBackground == "a Spy") {playerPersonality = "You are incredibly slow to trust. Those who seem the fairest often have the most to hide.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Criminal" || playerPersonalityResult == 6 && playerBackground == "a Spy") {playerPersonality = "You don't pay attention to the risks in a situation. Never tell you the odds.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Criminal" || playerPersonalityResult == 7 && playerBackground == "a Spy") {playerPersonality = "The best way to get you to do something is to tell you that you can't do it.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Criminal" || playerPersonalityResult == 8 && playerBackground == "a Spy") {playerPersonality = "You blow up at the slightest insult.";}

		if (playerIdealResult == 1 && playerBackground == "a Criminal" || playerIdealResult == 1 && playerBackground == "a Spy") {playerIdeal = "Honor. You don't steal from others in the trade. (Lawful)";}
		if (playerIdealResult == 2 && playerBackground == "a Criminal" || playerIdealResult == 2 && playerBackground == "a Spy") {playerIdeal = "Freedom. Chains are meant to be broken, as are those who would forge them. (Chaotic)";}
		if (playerIdealResult == 3 && playerBackground == "a Criminal" || playerIdealResult == 3 && playerBackground == "a Spy") {playerIdeal = "Charity. You steal from the wealthy so that you can help people in need. (Good)";}
		if (playerIdealResult == 4 && playerBackground == "a Criminal" || playerIdealResult == 4 && playerBackground == "a Spy") {playerIdeal = "Greed. You will do whatever it takes to become wealthy. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Criminal" || playerIdealResult == 5 && playerBackground == "a Spy") {playerIdeal = "People. You're loyal to your friends, not to any ideals, and everyone else can take a trip down the Styx for all you care. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Criminal" || playerIdealResult == 6 && playerBackground == "a Spy") {playerIdeal = "Redemption. There's a spark of good in everyone. (Good)";}

		if (playerBondResult == 1 && playerBackground == "a Criminal" || playerBondResult == 1 && playerBackground == "a Spy" ) {playerBond = "You're trying to pay off an old debt you owe to a generous benefactor.";}
		if (playerBondResult == 2 && playerBackground == "a Criminal" || playerBondResult == 2 && playerBackground == "a Spy" ) {playerBond = "Your ill-gotten gains go to support your family.";}
		if (playerBondResult == 3 && playerBackground == "a Criminal" || playerBondResult == 3 && playerBackground == "a Spy" ) {playerBond = "Something important was taken from you, and you aim to steal it back.";}
		if (playerBondResult == 4 && playerBackground == "a Criminal" || playerBondResult == 4 && playerBackground == "a Spy" ) {playerBond = "You will become the greatest thief that ever lived.";}
		if (playerBondResult == 5 && playerBackground == "a Criminal" || playerBondResult == 5 && playerBackground == "a Spy" ) {playerBond = "You're guilty of a terrible crime. You hope you can redeem yourself for it.";}
		if (playerBondResult == 6 && playerBackground == "a Criminal" || playerBondResult == 6 && playerBackground == "a Spy" ) {playerBond = "Someone you love died because of a mistake you made. That will never happen again.";}

		if (playerFlawResult == 1 && playerBackground == "a Criminal" || playerFlawResult == 1 && playerBackground == "a Spy") {playerFlaw = "When you see something valuable, you can't think about anything but how to steal it.";}
		if (playerFlawResult == 2 && playerBackground == "a Criminal" || playerFlawResult == 2 && playerBackground == "a Spy") {playerFlaw = "When faced with a choice between money and your friends, you usually choose the money.";}
		if (playerFlawResult == 3 && playerBackground == "a Criminal" || playerFlawResult == 3 && playerBackground == "a Spy") {playerFlaw = "If there's a plan, you'll forget it. If you don't forget it, you'll ignore it.";}
		if (playerFlawResult == 4 && playerBackground == "a Criminal" || playerFlawResult == 4 && playerBackground == "a Spy") {playerFlaw = "You have a 'tell' that reveals when you're lying.";}
		if (playerFlawResult == 5 && playerBackground == "a Criminal" || playerFlawResult == 5 && playerBackground == "a Spy") {playerFlaw = "You turn tail and run when things look bad.";}
		if (playerFlawResult == 6 && playerBackground == "a Criminal" || playerFlawResult == 6 && playerBackground == "a Spy") {playerFlaw = "An innocent person is in prison for a crime that you committed. You're okay with that.";}

		if (backgroundResult == 4 && flip == 1) {playerBackground = "an Entertainer";}
		if (backgroundResult == 4 && flip == 2) {playerBackground = "a Gladiator";}
		if (playerBackground == "an Entertainer") {playerBackgroundSpecialResult = roll(1,10); playerBackgroundSpecial = "Entertainer Routine: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Actor";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Dancer";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Fire-eater";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Jester";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Juggler";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Instrumentalist";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Poet";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Singer";}
		if (playerBackgroundSpecialResult == 9 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Storyteller";}
		if (playerBackgroundSpecialResult == 10 && playerBackground == "an Entertainer") {playerBackgroundSpecial += "Tumbler";}

		if (playerPersonalityResult == 1 && playerBackground == "an Entertainer" || playerPersonalityResult == 1 && playerBackground == "a Gladiator") {playerPersonality = "You know a story relevant to almost every situation.";}
		if (playerPersonalityResult == 2 && playerBackground == "an Entertainer" || playerPersonalityResult == 2 && playerBackground == "a Gladiator") {playerPersonality = "Whenever I come to a new place, you collect local rumors and spread gossip.";}
		if (playerPersonalityResult == 3 && playerBackground == "an Entertainer" || playerPersonalityResult == 3 && playerBackground == "a Gladiator") {playerPersonality = "You're a hopeless romantic, always searching for that 'special someone.'";}
		if (playerPersonalityResult == 4 && playerBackground == "an Entertainer" || playerPersonalityResult == 4 && playerBackground == "a Gladiator") {playerPersonality = "Nobody stays angry at you or around you for long, since you can defuse any amount of tension.";}
		if (playerPersonalityResult == 5 && playerBackground == "an Entertainer" || playerPersonalityResult == 5 && playerBackground == "a Gladiator") {playerPersonality = "You love a good insult, even one directed at you.";}
		if (playerPersonalityResult == 6 && playerBackground == "an Entertainer" || playerPersonalityResult == 6 && playerBackground == "a Gladiator") {playerPersonality = "You get bitter if you're not the center of attention.";}
		if (playerPersonalityResult == 7 && playerBackground == "an Entertainer" || playerPersonalityResult == 7 && playerBackground == "a Gladiator") {playerPersonality = "You'll settle for nothing less than perfection.";}
		if (playerPersonalityResult == 8 && playerBackground == "an Entertainer" || playerPersonalityResult == 8 && playerBackground == "a Gladiator") {playerPersonality = "You change your mood or your mind as quickly as you change key in a song.";}

		if (playerIdealResult == 1 && playerBackground == "an Entertainer" || playerIdealResult == 1 && playerBackground == "a Gladiator") {playerIdeal = "Beauty. When you perform, you make the world better than it was. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "an Entertainer" || playerIdealResult == 2 && playerBackground == "a Gladiator") {playerIdeal = "Tradition. The stories, legends, and songs of the past must never be forgotten, for they teach us who we are. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "an Entertainer" || playerIdealResult == 3 && playerBackground == "a Gladiator") {playerIdeal = "Creativity. The world is in need of new ideas and bold action. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "an Entertainer" || playerIdealResult == 4 && playerBackground == "a Gladiator") {playerIdeal = "Greed. You're only in it for the money and fame. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "an Entertainer" || playerIdealResult == 5 && playerBackground == "a Gladiator") {playerIdeal = "People. You like seeing the smiles on people's faces when you perform. That's all that matters. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "an Entertainer" || playerIdealResult == 6 && playerBackground == "a Gladiator") {playerIdeal = "Honesty. Art should reflect the soul; it should come from within and reveal who we really are. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "an Entertainer" || playerBondResult == 1 && playerBackground == "a Gladiator" ) {playerBond = "Your instrument is your most treasured possession, and it reminds you of someone you love.";}
		if (playerBondResult == 2 && playerBackground == "an Entertainer" || playerBondResult == 2 && playerBackground == "a Gladiator" ) {playerBond = "Someone stole your precious instrument, and someday you'll get it back.";}
		if (playerBondResult == 3 && playerBackground == "an Entertainer" || playerBondResult == 3 && playerBackground == "a Gladiator" ) {playerBond = "You want to be famous, whatever it takes.";}
		if (playerBondResult == 4 && playerBackground == "an Entertainer" || playerBondResult == 4 && playerBackground == "a Gladiator" ) {playerBond = "You idolize a hero of the old tales and measure your deeds against that person's.";}
		if (playerBondResult == 5 && playerBackground == "an Entertainer" || playerBondResult == 5 && playerBackground == "a Gladiator" ) {playerBond = "You will do anything to prove yourself superior to your hated rival.";}
		if (playerBondResult == 6 && playerBackground == "an Entertainer" || playerBondResult == 6 && playerBackground == "a Gladiator" ) {playerBond = "You would do anything for the other members of your old troupe.";}

		if (playerFlawResult == 1 && playerBackground == "an Entertainer" || playerFlawResult == 1 && playerBackground == "a Gladiator") {playerFlaw = "You'll do anything to win fame and renown.";}
		if (playerFlawResult == 2 && playerBackground == "an Entertainer" || playerFlawResult == 2 && playerBackground == "a Gladiator") {playerFlaw = "You're a sucker for a pretty face.";}
		if (playerFlawResult == 3 && playerBackground == "an Entertainer" || playerFlawResult == 3 && playerBackground == "a Gladiator") {playerFlaw = "A scandal prevents you from ever going home again. That kind of trouble seems to follow you around.";}
		if (playerFlawResult == 4 && playerBackground == "an Entertainer" || playerFlawResult == 4 && playerBackground == "a Gladiator") {playerFlaw = "You once satirized a noble who still wants your head. It was a mistake that you will likely repeat.";}
		if (playerFlawResult == 5 && playerBackground == "an Entertainer" || playerFlawResult == 5 && playerBackground == "a Gladiator") {playerFlaw = "You have trouble keeping your true feelings hidden. Your sharp tongue lands you in trouble.";}
		if (playerFlawResult == 6 && playerBackground == "an Entertainer" || playerFlawResult == 6 && playerBackground == "a Gladiator") {playerFlaw = "Despite your best efforts, you are unreliable to your friends.";}

		if (backgroundResult == 5) {playerBackground = "a Folk Hero";}
		if (playerBackground == "a Folk Hero") {playerBackgroundSpecialResult = roll(1,10); playerBackgroundSpecial = "Defining Event: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You stood up to a tyrant's agents.";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You saved people during a natural disaster.";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You stood alone against a terrible monster.";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You stole from a corrupt merchant to help the poor.";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You led a militia to fight off an invading army.";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You broke into a tyrant's castle and stole weapons to arm the people.";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "You trained the peasantry to use farm implements as weapons against a tyrant's soldiers.";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "A lord rescinded an unpopular decree after you led a symbolic act of protect against it.";}
		if (playerBackgroundSpecialResult == 9 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "A celestial, fey, or similar creature gave you a blessing or revealed your secret origin.";}
		if (playerBackgroundSpecialResult == 10 && playerBackground == "a Folk Hero") {playerBackgroundSpecial += "Recruited into a lord's army, you rose to leadership and were commended for your heroism.";}

		if (playerPersonalityResult == 1 && playerBackground == "a Folk Hero") {playerPersonality = "You judge people by their actions, not their words.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Folk Hero") {playerPersonality = "If someone is in trouble, you're always ready to lend help.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Folk Hero") {playerPersonality = "When you set your mind to something, you follow through no matter what gets in your way.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Folk Hero") {playerPersonality = "You have a strong sense of fair play and always try to find the most equitable solution to arguments.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Folk Hero") {playerPersonality = "You're confident in your own abilities and do what you can to instill confidence in others.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Folk Hero") {playerPersonality = "Thinking is for other people. You prefer action.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Folk Hero") {playerPersonality = "You misuse long words in an attempt to sound smarter.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Folk Hero") {playerPersonality = "You get bored easily. When are you going to get on with your destiny?";}

		if (playerIdealResult == 1 && playerBackground == "a Folk Hero") {playerIdeal = "Respect. People deserve to be treated with dignity and respect. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "a Folk Hero") {playerIdeal = "Fairness. No one should get preferential treatment before the law, and no one is above the law. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Folk Hero") {playerIdeal = "Freedom. Tyrants must not be allowed to oppress the people. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Folk Hero") {playerIdeal = "Might. If you become strong, you can take what you want--what you deserve. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Folk Hero") {playerIdeal = "Sincerity. There's no good in pretending to be something you're not. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Folk Hero") {playerIdeal = "Destiny. Nothing and no one can steer you away from your higher calling. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Folk Hero") {playerBond = "You have a family, but you have no idea where they are. One day, you hope to see them again.";}
		if (playerBondResult == 2 && playerBackground == "a Folk Hero") {playerBond = "You worked the land, you love the land, and you will protect the land.";}
		if (playerBondResult == 3 && playerBackground == "a Folk Hero") {playerBond = "A proud noble once gave you a horrible beating, and you will take your revenge on any bully you encounter.";}
		if (playerBondResult == 4 && playerBackground == "a Folk Hero") {playerBond = "Your tools are symbols of your past life, and you carry them so that you will never forget your roots.";}
		if (playerBondResult == 5 && playerBackground == "a Folk Hero") {playerBond = "You protect those who cannot protect themselves.";}
		if (playerBondResult == 6 && playerBackground == "a Folk Hero") {playerBond = "You wish your childhood sweetheart had come with you to pursue your destiny.";}

		if (playerFlawResult == 1 && playerBackground == "a Folk Hero") {playerFlaw = "The tyrant who rules your land will stop at nothing to see you killed.";}
		if (playerFlawResult == 2 && playerBackground == "a Folk Hero") {playerFlaw = "You're convinced of the significance of your destiny, and blind to your shortcomings and the risk of failure.";}
		if (playerFlawResult == 3 && playerBackground == "a Folk Hero") {playerFlaw = "The people who knew you when you were young know your shameful secret, so you can never go home again.";}
		if (playerFlawResult == 4 && playerBackground == "a Folk Hero") {playerFlaw = "You have a weakness for the vices of the city, especially hard drink.";}
		if (playerFlawResult == 5 && playerBackground == "a Folk Hero") {playerFlaw = "Secretly, you believe that things would be better if you were a tyrant lording over the land.";}
		if (playerFlawResult == 6 && playerBackground == "a Folk Hero") {playerFlaw = "You have trouble trusting in your allies.";}

		if (backgroundResult == 6 && flip == 1) {playerBackground = "a Guild Artisan";}
		if (backgroundResult == 6 && flip == 2) {playerBackground = "a Guild Merchant";}
		if (playerBackground == "a Guild Artisan" || playerBackground == "a Guild Merchant") {playerBackgroundSpecialResult = roll(1,20); playerBackgroundSpecial = "Guild Business: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 1 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Alchemists and apothecaries";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 2 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Armorers, locksmiths, and finesmiths";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 3 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Brewers, distillers, and vintners";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 4 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Calligraphers, scribes, and scriveners";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 5 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Carpenters, roofers, and plasterers";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 6 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Cartographers, surveyors, and chart-makers";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 7 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Cobblers and shoemakers";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 8 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Cooks and bakers";}
		if (playerBackgroundSpecialResult == 9 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 9 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Glassblowers and glaziers";}
		if (playerBackgroundSpecialResult == 10 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 10 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Jewelers and gemcutters";}
		if (playerBackgroundSpecialResult == 11 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 11 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Leatherworkers, skinners, and tanners";}
		if (playerBackgroundSpecialResult == 12 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 12 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Masons and stonecutters";}
		if (playerBackgroundSpecialResult == 13 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 13 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Painters, limners, and sign-makers";}
		if (playerBackgroundSpecialResult == 14 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 14 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Potters and tile-makers";}
		if (playerBackgroundSpecialResult == 15 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 15 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Shipwrights and sailmakers";}
		if (playerBackgroundSpecialResult == 16 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 16 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Smiths and metal-forgers";}
		if (playerBackgroundSpecialResult == 17 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 17 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Tinkers, pewterers, and casters";}
		if (playerBackgroundSpecialResult == 18 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 18 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Wagon-makers and wheelwrights";}
		if (playerBackgroundSpecialResult == 19 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 19 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Weavers and dyers";}
		if (playerBackgroundSpecialResult == 20 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 20 && playerBackground == "a Guild Merchant") {playerBackgroundSpecial += "Woodcarvers, coopers, and bowyers";}

		if (playerPersonalityResult == 1 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 1 && playerBackground == "a Guild Merchant") {playerPersonality = "You believe that anything worth doing is worth doing right. You can't help it--you're a perfectionist.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 2 && playerBackground == "a Guild Merchant") {playerPersonality = "You're a snob who looks down on those who can't appreciate fine art.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 3 && playerBackground == "a Guild Merchant") {playerPersonality = "You always want to know how things work and what makes people tick.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 4 && playerBackground == "a Guild Merchant") {playerPersonality = "You're full of witty aphorisms and have a proverb for every occasion.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 5 && playerBackground == "a Guild Merchant") {playerPersonality = "You're rude to people who lack your commitment to hard work and fair play.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 6 && playerBackground == "a Guild Merchant") {playerPersonality = "You like to talk at length about your profession.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 7 && playerBackground == "a Guild Merchant") {playerPersonality = "You don't part with your money easily and will haggle tirelessly to get the best deal possible.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Guild Artisan" || playerPersonalityResult == 8 && playerBackground == "a Guild Merchant") {playerPersonality = "You're well known for your work, and you want to make sure everyone appreciates it. You're always taken aback when people haven't heard of you.";}

		if (playerIdealResult == 1 && playerBackground == "a Guild Artisan" || playerIdealResult == 1 && playerBackground == "a Guild Merchant") {playerIdeal = "Community. It is the duty of all civilized people to strengthen the bonds of community and the security of civilization. (Lawful)";}
		if (playerIdealResult == 2 && playerBackground == "a Guild Artisan" || playerIdealResult == 2 && playerBackground == "a Guild Merchant") {playerIdeal = "Generosity. Your talents were given to you so that you could use them to benefit the world. (Good)";}
		if (playerIdealResult == 3 && playerBackground == "a Guild Artisan" || playerIdealResult == 3 && playerBackground == "a Guild Merchant") {playerIdeal = "Freedom. Everyone should be free to pursue his or her own livelihood. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Guild Artisan" || playerIdealResult == 4 && playerBackground == "a Guild Merchant") {playerIdeal = "Greed. You're only in it for the money. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Guild Artisan" || playerIdealResult == 5 && playerBackground == "a Guild Merchant") {playerIdeal = "People. You're committed to the people you care about, not to ideals. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Guild Artisan" || playerIdealResult == 6 && playerBackground == "a Guild Merchant") {playerIdeal = "Aspiration. You work hard to be the best there is at your craft.";}

		if (playerBondResult == 1 && playerBackground == "a Guild Artisan" || playerBondResult == 1 && playerBackground == "a Guild Merchant") {playerBond = "The workshop where you learned your trade is the most important place in the world to you.";}
		if (playerBondResult == 2 && playerBackground == "a Guild Artisan" || playerBondResult == 2 && playerBackground == "a Guild Merchant") {playerBond = "You created a great work for someone, and then found them unworthy to receive it. You're still looking for someone worthy.";}
		if (playerBondResult == 3 && playerBackground == "a Guild Artisan" || playerBondResult == 3 && playerBackground == "a Guild Merchant") {playerBond = "You owe your guild a great debt for forging you into the person you am today.";}
		if (playerBondResult == 4 && playerBackground == "a Guild Artisan" || playerBondResult == 4 && playerBackground == "a Guild Merchant") {playerBond = "You pursue wealth to secure someone's love.";}
		if (playerBondResult == 5 && playerBackground == "a Guild Artisan" || playerBondResult == 5 && playerBackground == "a Guild Merchant") {playerBond = "One day you will return to your guild and prove that you are the greatest artisan of them all.";}
		if (playerBondResult == 6 && playerBackground == "a Guild Artisan" || playerBondResult == 6 && playerBackground == "a Guild Merchant") {playerBond = "You will get revenge on the evil forces that destroyed your place of business and ruined your livelihood.";}

		if (playerFlawResult == 1 && playerBackground == "a Guild Artisan" || playerFlawResult == 1 && playerBackground == "a Guild Merchant") {playerFlaw = "You'll do anything to get your hands on something rare or priceless.";}
		if (playerFlawResult == 2 && playerBackground == "a Guild Artisan" || playerFlawResult == 2 && playerBackground == "a Guild Merchant") {playerFlaw = "You're quick to assume that someone is trying to cheat you.";}
		if (playerFlawResult == 3 && playerBackground == "a Guild Artisan" || playerFlawResult == 3 && playerBackground == "a Guild Merchant") {playerFlaw = "No one must ever learn that you once stole money from guild coffers.";}
		if (playerFlawResult == 4 && playerBackground == "a Guild Artisan" || playerFlawResult == 4 && playerBackground == "a Guild Merchant") {playerFlaw = "You're never satisfied with what you have--you always want more.";}
		if (playerFlawResult == 5 && playerBackground == "a Guild Artisan" || playerFlawResult == 5 && playerBackground == "a Guild Merchant") {playerFlaw = "You would kill to acquire a noble title.";}
		if (playerFlawResult == 6 && playerBackground == "a Guild Artisan" || playerFlawResult == 6 && playerBackground == "a Guild Merchant") {playerFlaw = "You're horribly jealous of anyone who can outshine your handiwork. Everywhere you go, you're surrounded by rivals.";}

		if (backgroundResult == 7) {playerBackground = "a Hermit";}
		if (playerBackground == "a Hermit") {playerBackgroundSpecialResult = roll(1,8); playerBackgroundSpecial = "Life of Seclusion: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You were searching for spiritual enlightenment.";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You were partaking of communal living in accordance with the dictates of a religious order.";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You were exiled for a crime you didn't commit.";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You retreated from society after a life-altering event.";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You needed a quiet place to work on your art, literature, music, or manifesto.";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You needed to commune with nature, far from civilization.";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You were the caretaker of an ancient ruin or relic.";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Hermit") {playerBackgroundSpecial += "You were a pilgrim in search of a person, place, or relic of spiritual significance.";}

		if (playerPersonalityResult == 1 && playerBackground == "a Hermit") {playerPersonality = "You've been isolated for so long that you rarely speak, preferring gestures and the occasional grunt.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Hermit") {playerPersonality = "You are utterly serene, even in the face of disaster.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Hermit") {playerPersonality = "The leader of your community had something wise to say on every topic, and you are eager to share that wisdom.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Hermit") {playerPersonality = "You feel tremendous empathy for all who suffer.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Hermit") {playerPersonality = "You're oblivious to etiquette and social expectations. ";}
		if (playerPersonalityResult == 6 && playerBackground == "a Hermit") {playerPersonality = "You connect everything that happens to you to a grand, cosmic plan.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Hermit") {playerPersonality = "You often get lost in your own thoughts and contemplation, beeoming oblivious to your surroundings.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Hermit") {playerPersonality = "You are working on a grand philosophical theory and love sharing your ideas.";}

		if (playerIdealResult == 1 && playerBackground == "a Hermit") {playerIdeal = "Greater Good. Your gifts are meant to be shared with all, not used for your own benefit. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "a Hermit") {playerIdeal = "Logic. Emotions must not cloud your sense of what is right and true, or your logical thinking. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Hermit") {playerIdeal = "Free Thinking. Inquiry and curiosity are the pillars of progress. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Hermit") {playerIdeal = "Power. Solitude and contemplation are paths toward mystical or magical power. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Hermit") {playerIdeal = "Live and Let Live. Meddling in the affairs of others only causes trouble. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Hermit") {playerIdeal = "Self Knowledge. If you know yourself, there's nothing left to know. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Hermit") {playerBond = "Nothing is more important than the other members of your hermitage, order, or association.";}
		if (playerBondResult == 2 && playerBackground == "a Hermit") {playerBond = "You entered seclusion to hide from the ones who might still be hunting you. You must someday confront them.";}
		if (playerBondResult == 3 && playerBackground == "a Hermit") {playerBond = "You're still seeking the enlightenment you pursued in your seclusion, and it still eludes you.";}
		if (playerBondResult == 4 && playerBackground == "a Hermit") {playerBond = "You entered seclusion because you loved someone you could not have.";}
		if (playerBondResult == 5 && playerBackground == "a Hermit") {playerBond = "Should your discovery come to light, it could bring ruin to the world.";}
		if (playerBondResult == 6 && playerBackground == "a Hermit") {playerBond = "Your isolation gave you great insight into a great evil that only you can destroy.";}

		if (playerFlawResult == 1 && playerBackground == "a Hermit") {playerFlaw = "Now that you've returned to the world, you enjoy its delights a little too much.";}
		if (playerFlawResult == 2 && playerBackground == "a Hermit") {playerFlaw = "You harbor dark, bloodthirsty thoughts that your isolation and meditation failed to quell.";}
		if (playerFlawResult == 3 && playerBackground == "a Hermit") {playerFlaw = "You are dogmatic in your thoughts and philosophy.";}
		if (playerFlawResult == 4 && playerBackground == "a Hermit") {playerFlaw = "You let your need to win arguments overshadow friendships and harmony.";}
		if (playerFlawResult == 5 && playerBackground == "a Hermit") {playerFlaw = "You'd risk too much to uncover a lost bit of knowledge.";}
		if (playerFlawResult == 6 && playerBackground == "a Hermit") {playerFlaw = "You like keeping secrets and won't share them with anyone.";}


		if (backgroundResult == 8 && flip == 1) {playerBackground = "a Noble";}
		if (backgroundResult == 8 && flip == 2) {playerBackground = "a Knight";}
		if (playerBackground == "a Noble" || playerBackground == "a Knight") {playerBackgroundSpecial = null;}

		if (playerPersonalityResult == 1 && playerBackground == "a Noble" || playerPersonalityResult == 1 && playerBackground == "a Knight") {playerPersonality = "Your eloquent flattery makes everyone you talk to feel like the most wonderful and important person in the world.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Noble" || playerPersonalityResult == 2 && playerBackground == "a Knight") {playerPersonality = "The common folk love you for your kindness and generosity.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Noble" || playerPersonalityResult == 3 && playerBackground == "a Knight") {playerPersonality = "No one could doubt by looking at your regal bearing that you are a cut above the unwashed masses.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Noble" || playerPersonalityResult == 4 && playerBackground == "a Knight") {playerPersonality = "You take great pains to always look your best and follow the latest fashions.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Noble" || playerPersonalityResult == 5 && playerBackground == "a Knight") {playerPersonality = "You don't like to get your hands dirty, and you won't be caught dead in unsuitable accommodations.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Noble" || playerPersonalityResult == 6 && playerBackground == "a Knight") {playerPersonality = "Despite your noble birth, you do not place yourself above other folk. You all have the same blood.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Noble" || playerPersonalityResult == 7 && playerBackground == "a Knight") {playerPersonality = "Your favor, once lost, is lost forever. ";}
		if (playerPersonalityResult == 8 && playerBackground == "a Noble" || playerPersonalityResult == 8 && playerBackground == "a Knight") {playerPersonality = "If anyone does you an injury, you will crush them, ruin their name, and salt their fields.";}

		if (playerIdealResult == 1 && playerBackground == "a Noble" || playerIdealResult == 1 && playerBackground == "a Knight") {playerIdeal = "Respect. Respect is due to you beeause of your position, but all people regardless of station deserve to be treated with dignity. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "a Noble" || playerIdealResult == 2 && playerBackground == "a Knight") {playerIdeal = "Responsibility. It is your duty to respect the authority of those above you, just as those below you must respect yours. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Noble" || playerIdealResult == 3 && playerBackground == "a Knight") {playerIdeal = "Independence. You must prove that you can handle yourself without the coddling of your family. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Noble" || playerIdealResult == 4 && playerBackground == "a Knight") {playerIdeal = "Power. If you can attain more power, no one will tell you what to do. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Noble" || playerIdealResult == 5 && playerBackground == "a Knight") {playerIdeal = "Family. Blood runs thicker than water. (Any alignment)";}
		if (playerIdealResult == 6 && playerBackground == "a Noble" || playerIdealResult == 6 && playerBackground == "a Knight") {playerIdeal = "Noble Obligation. It is your duty to protect and care for the people beneath you. (Good)";}

		if (playerBondResult == 1 && playerBackground == "a Noble" || playerBondResult == 1 && playerBackground == "a Knight") {playerBond = "You will face any challenge to win the approval of your family.";}
		if (playerBondResult == 2 && playerBackground == "a Noble" || playerBondResult == 2 && playerBackground == "a Knight") {playerBond = "Your house's alliance with another noble family must be sustained at all costs.";}
		if (playerBondResult == 3 && playerBackground == "a Noble" || playerBondResult == 3 && playerBackground == "a Knight") {playerBond = "Nothing is more important than the other members of your family.";}
		if (playerBondResult == 4 && playerBackground == "a Noble" || playerBondResult == 4 && playerBackground == "a Knight") {playerBond = "You are in love with the heir of a family that your family despises.";}
		if (playerBondResult == 5 && playerBackground == "a Noble" || playerBondResult == 5 && playerBackground == "a Knight") {playerBond = "Your loyalty to your sovereign is unwavering.";}
		if (playerBondResult == 6 && playerBackground == "a Noble" || playerBondResult == 6 && playerBackground == "a Knight") {playerBond = "The common folk must see you as a hero of the people.";}

		if (playerFlawResult == 1 && playerBackground == "a Noble" || playerFlawResult == 1 && playerBackground == "a Knight") {playerFlaw = "You secretly believe that everyone is beneath you.";}
		if (playerFlawResult == 2 && playerBackground == "a Noble" || playerFlawResult == 2 && playerBackground == "a Knight") {playerFlaw = "You hide a truly scandalous secret that could ruin your family forever.";}
		if (playerFlawResult == 3 && playerBackground == "a Noble" || playerFlawResult == 3 && playerBackground == "a Knight") {playerFlaw = "You too often hear veiled insults and threats in every word addressed to you, and you're quick to anger.";}
		if (playerFlawResult == 4 && playerBackground == "a Noble" || playerFlawResult == 4 && playerBackground == "a Knight") {playerFlaw = "You have an insatiable desire for carnal pleasures.";}
		if (playerFlawResult == 5 && playerBackground == "a Noble" || playerFlawResult == 5 && playerBackground == "a Knight") {playerFlaw = "In fact, the world does revolve around you.";}
		if (playerFlawResult == 6 && playerBackground == "a Noble" || playerFlawResult == 6 && playerBackground == "a Knight") {playerFlaw = "By your words and actions, you often bring shame to your family.";}

		if (backgroundResult == 9) {playerBackground = "an Outlander";}
		if (playerBackground == "an Outlander") {playerBackgroundSpecialResult = roll(1,10); playerBackgroundSpecial = "Origin: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Forester";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Trapper";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Homesteader";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Guide";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Exile or outcast";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Bounty hunter";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Pilgrim";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Tribal nomad";}
		if (playerBackgroundSpecialResult == 9 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Hunter-gatherer";}
		if (playerBackgroundSpecialResult == 10 && playerBackground == "an Outlander") {playerBackgroundSpecial += "Tribal marauder";}

		if (playerPersonalityResult == 1 && playerBackground == "an Outlander") {playerPersonality = "You're driven by a wanderlust that led you away from home.";}
		if (playerPersonalityResult == 2 && playerBackground == "an Outlander") {playerPersonality = "You watch over your friends as if they were a litter of newborn pups.";}
		if (playerPersonalityResult == 3 && playerBackground == "an Outlander") {playerPersonality = "You once ran twenty-five miles without stopping to warn to your clan of an approaching orc horde. You'd do it again if you had to.";}
		if (playerPersonalityResult == 4 && playerBackground == "an Outlander") {playerPersonality = "You have a lesson for every situation, drawn from observing nature.";}
		if (playerPersonalityResult == 5 && playerBackground == "an Outlander") {playerPersonality = "You place no stock in wealthy or well-mannered folk. Money and manners won't save you from a hungry owlbear.";}
		if (playerPersonalityResult == 6 && playerBackground == "an Outlander") {playerPersonality = "You're always picking things up, absently fiddling with them, and sometimes accidentally breaking them.";}
		if (playerPersonalityResult == 7 && playerBackground == "an Outlander") {playerPersonality = "You feel far more comfortable around animals than people.";}
		if (playerPersonalityResult == 8 && playerBackground == "an Outlander") {playerPersonality = "You were, in fact, raised by wolves.";}

		if (playerIdealResult == 1 && playerBackground == "an Outlander") {playerIdeal = "Change. Life is like the seasons, in constant change, and we must change with it. (Chaotic)";}
		if (playerIdealResult == 2 && playerBackground == "an Outlander") {playerIdeal = "Greater Good. It is each person's responsibility to make the most happiness for the whole tribe. (Good)";}
		if (playerIdealResult == 3 && playerBackground == "an Outlander") {playerIdeal = "Honor. If you dishonor yourself, you dishonor your whole clan. (Lawful)";}
		if (playerIdealResult == 4 && playerBackground == "an Outlander") {playerIdeal = "Might. The strongest are meant to rule. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "an Outlander") {playerIdeal = "Nature. The natural world is more important than all the constructs of civilization. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "an Outlander") {playerIdeal = "Glory. You must earn glory in battle, for yourself and your clan. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "an Outlander") {playerBond = "Your family, clan, or tribe is the most important thing in your life, even when they are far from you.";}
		if (playerBondResult == 2 && playerBackground == "an Outlander") {playerBond = "An injury to the unspoiled wilderness of your home is an injury to you.";}
		if (playerBondResult == 3 && playerBackground == "an Outlander") {playerBond = "You will bring terrible wrath down on the evildoers who destroyed your homeland.";}
		if (playerBondResult == 4 && playerBackground == "an Outlander") {playerBond = "You are the last of your tribe, and it is up to you to ensure their names enter legend.";}
		if (playerBondResult == 5 && playerBackground == "an Outlander") {playerBond = "You suffer awful visions of a coming disaster and will do anything to prevent it.";}
		if (playerBondResult == 6 && playerBackground == "an Outlander") {playerBond = "It is your duty to provide children to sustain my tribe.";}

		if (playerFlawResult == 1 && playerBackground == "an Outlander") {playerFlaw = "You are too enamored of ale, wine, and other intoxicants.";}
		if (playerFlawResult == 2 && playerBackground == "an Outlander") {playerFlaw = "There's no room for caution in a life lived to the fullest.";}
		if (playerFlawResult == 3 && playerBackground == "an Outlander") {playerFlaw = "You remember every insult you've received and nurse a silent resentment toward anyone who's ever wronged you.";}
		if (playerFlawResult == 4 && playerBackground == "an Outlander") {playerFlaw = "You are slow to trust members of other races, tribes, and societies.";}
		if (playerFlawResult == 5 && playerBackground == "an Outlander") {playerFlaw = "Violence is your answer to almost any challenge. ";}
		if (playerFlawResult == 6 && playerBackground == "an Outlander") {playerFlaw = "Don't expect you to save those who can't save themselves. It is nature's way that the strong thrive and the weak perish.";}

		if (backgroundResult == 10) {playerBackground = "a Sage";}
		if (playerBackground == "a Sage") {playerBackgroundSpecialResult = roll(1,8); playerBackgroundSpecial = "Specialty: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Sage") {playerBackgroundSpecial += "Alchemist";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Sage") {playerBackgroundSpecial += "Astronomer";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Sage") {playerBackgroundSpecial += "Discredited academic";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Sage") {playerBackgroundSpecial += "Librarian";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Sage") {playerBackgroundSpecial += "Professor";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Sage") {playerBackgroundSpecial += "Researcher";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Sage") {playerBackgroundSpecial += "Wizard's apprentice";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Sage") {playerBackgroundSpecial += "Scribe";}

		if (playerPersonalityResult == 1 && playerBackground == "a Sage") {playerPersonality = "You use polysyllabic words that convey the impression of great erudition.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Sage") {playerPersonality = "You've read every book in the world's greatest libraries--or you like to boast that you have.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Sage") {playerPersonality = "You're used to helping out those who aren't as smart as you are, and you patiently explain anything and everything to others.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Sage") {playerPersonality = "There's nothing you like more than a good mystery.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Sage") {playerPersonality = "You're willing to listen to every side of an argument before you make your own judgment.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Sage") {playerPersonality = "You...speak...slowly...when talking...to idiots...which...almost...everyone...is...compared...to you.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Sage") {playerPersonality = "You are horribly, horribly awkward in social situations.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Sage") {playerPersonality = "You're convinced that people are always trying to steal your secrets.";}

		if (playerIdealResult == 1 && playerBackground == "a Sage") {playerIdeal = "Knowledge. The path to power and self-improvement is through knowledge. (Neutral)";}
		if (playerIdealResult == 2 && playerBackground == "a Sage") {playerIdeal = "Beauty. What is beautiful points us beyond itself toward what is true. (Good)";}
		if (playerIdealResult == 3 && playerBackground == "a Sage") {playerIdeal = "Logic. Emotions must not cloud our logical thinking. (Lawful)";}
		if (playerIdealResult == 4 && playerBackground == "a Sage") {playerIdeal = "No Limits. Nothing should fetter the infinite possibility inherent in all existence. (Chaotic)";}
		if (playerIdealResult == 5 && playerBackground == "a Sage") {playerIdeal = "Power. Knowledge is the path to power and domination. (Evil)";}
		if (playerIdealResult == 6 && playerBackground == "a Sage") {playerIdeal = "Self Improvement. The goal of a life of study is the betterment of oneself. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Sage") {playerBond = "It is your duty to protect your students.";}
		if (playerBondResult == 2 && playerBackground == "a Sage") {playerBond = "You have an ancient text that holds terrible secrets that must not fall into the wrong hands.";}
		if (playerBondResult == 3 && playerBackground == "a Sage") {playerBond = "You work to preserve a library, university, scriptorium, or monastery.";}
		if (playerBondResult == 4 && playerBackground == "a Sage") {playerBond = "Your life's work is a series of tomes related to a specific field of lore.";}
		if (playerBondResult == 5 && playerBackground == "a Sage") {playerBond = "You've been searching your whole life for the answer to a certain question.";}
		if (playerBondResult == 6 && playerBackground == "a Sage") {playerBond = "You sold your soul for knowledge. You hope to do great deeds and win it back.";}

		if (playerFlawResult == 1 && playerBackground == "a Sage") {playerFlaw = "You are easily distracted by the promise of information.";}
		if (playerFlawResult == 2 && playerBackground == "a Sage") {playerFlaw = "Most people scream and run when they see a demon. You stop and take notes on its anatomy.";}
		if (playerFlawResult == 3 && playerBackground == "a Sage") {playerFlaw = "Unlocking an ancient mystery is worth the price of a civilization.";}
		if (playerFlawResult == 4 && playerBackground == "a Sage") {playerFlaw = "You overlook obvious solutions in favor of complicated ones.";}
		if (playerFlawResult == 5 && playerBackground == "a Sage") {playerFlaw = "You speak without really thinking through your words, invariably insulting others.";}
		if (playerFlawResult == 6 && playerBackground == "a Sage") {playerFlaw = "You can't keep a secret to save your life, or anyone else's.";}

		if (backgroundResult == 11 && flip == 1) {playerBackground = "a Sailor";}
		if (backgroundResult == 11 && flip == 2) {playerBackground = "a Pirate";}
		if (playerBackground == "a Sailor" || playerBackground == "a Pirate") {playerBackgroundSpecial = null;}

		if (playerPersonalityResult == 1 && playerBackground == "a Sailor" || playerPersonalityResult == 1 && playerBackground == "a Pirate") {playerPersonality = "Your friends know they can rely on you, no matter what.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Sailor" || playerPersonalityResult == 2 && playerBackground == "a Pirate") {playerPersonality = "You work hard so that you can play hard when the work is done.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Sailor" || playerPersonalityResult == 3 && playerBackground == "a Pirate") {playerPersonality = "You enjoy sailing into new ports and making new friends over a flagon of ale.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Sailor" || playerPersonalityResult == 4 && playerBackground == "a Pirate") {playerPersonality = "You stretch the truth for the sake of a good story.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Sailor" || playerPersonalityResult == 5 && playerBackground == "a Pirate") {playerPersonality = "To you, a tavern brawl is a nice way to get to know a new city.";}
		if (playerPersonalityResult == 6 && playerBackground == "a Sailor" || playerPersonalityResult == 6 && playerBackground == "a Pirate") {playerPersonality = "You never pass up a friendly wager.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Sailor" || playerPersonalityResult == 7 && playerBackground == "a Pirate") {playerPersonality = "Your language is as foul as an otyugh nest.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Sailor" || playerPersonalityResult == 8 && playerBackground == "a Pirate") {playerPersonality = "You like a job well done, especially if you can convince someone else to do it.";}

		if (playerIdealResult == 1 && playerBackground == "a Sailor" || playerIdealResult == 1 && playerBackground == "a Pirate") {playerIdeal = "Respect. The thing that keeps a ship together is mutual respect between captain and crew. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "a Sailor" || playerIdealResult == 2 && playerBackground == "a Pirate") {playerIdeal = "Fairness. You all do the work, so you all share in the rewards. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Sailor" || playerIdealResult == 3 && playerBackground == "a Pirate") {playerIdeal = "Freedom. The sea is freedom--the freedom to go anywhere and do anything. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Sailor" || playerIdealResult == 4 && playerBackground == "a Pirate") {playerIdeal = "Mastery. You're a predator, and the other ships on the sea are your prey. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "a Sailor" || playerIdealResult == 5 && playerBackground == "a Pirate") {playerIdeal = "People. You're committed to your crewmates, not to ideals. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Sailor" || playerIdealResult == 6 && playerBackground == "a Pirate") {playerIdeal = "Aspiration. Someday you'll own your own ship and chart your own destiny. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Sailor" || playerBondResult == 1 && playerBackground == "a Pirate") {playerBond = "You're loyal to your captain first, everything else second.";}
		if (playerBondResult == 2 && playerBackground == "a Sailor" || playerBondResult == 2 && playerBackground == "a Pirate") {playerBond = "The ship is most important--crewmates and captains come and go.";}
		if (playerBondResult == 3 && playerBackground == "a Sailor" || playerBondResult == 3 && playerBackground == "a Pirate") {playerBond = "You'll always remember your first ship.";}
		if (playerBondResult == 4 && playerBackground == "a Sailor" || playerBondResult == 4 && playerBackground == "a Pirate") {playerBond = "In a harbor town, you have a paramour whose eyes nearly stole you from the sea.";}
		if (playerBondResult == 5 && playerBackground == "a Sailor" || playerBondResult == 5 && playerBackground == "a Pirate") {playerBond = "You were cheated out of your fair share of the profits, and you want to get your due.";}
		if (playerBondResult == 6 && playerBackground == "a Sailor" || playerBondResult == 6 && playerBackground == "a Pirate") {playerBond = "Ruthless pirates murdered your captain and crewmates, plundered your ship, and left you to die. Vengeance will be yours.";}

		if (playerFlawResult == 1 && playerBackground == "a Sailor" || playerFlawResult == 1 && playerBackground == "a Pirate") {playerFlaw = "You follow orders, even if you think they're wrong.";}
		if (playerFlawResult == 2 && playerBackground == "a Sailor" || playerFlawResult == 2 && playerBackground == "a Pirate") {playerFlaw = "You'll say anything to avoid having to do extra work.";}
		if (playerFlawResult == 3 && playerBackground == "a Sailor" || playerFlawResult == 3 && playerBackground == "a Pirate") {playerFlaw = "Once someone questions your courage, you never back down no matter how dangerous the situation.";}
		if (playerFlawResult == 4 && playerBackground == "a Sailor" || playerFlawResult == 4 && playerBackground == "a Pirate") {playerFlaw = "Once you start drinking, it's hard for you to stop.";}
		if (playerFlawResult == 5 && playerBackground == "a Sailor" || playerFlawResult == 5 && playerBackground == "a Pirate") {playerFlaw = "You can't help but pocket loose coins and other trinkets you come across.";}
		if (playerFlawResult == 6 && playerBackground == "a Sailor" || playerFlawResult == 6 && playerBackground == "a Pirate") {playerFlaw = "Your pride will probably lead to your destruction.";}

		if (backgroundResult == 12) {playerBackground = "a Soldier";}
		if (playerBackground == "a Soldier") {playerBackgroundSpecialResult = roll(1,8); playerBackgroundSpecial = "Military Specialty: ";}

		if (playerBackgroundSpecialResult == 1 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Officer";}
		if (playerBackgroundSpecialResult == 2 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Scout";}
		if (playerBackgroundSpecialResult == 3 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Infantry";}
		if (playerBackgroundSpecialResult == 4 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Cavalry";}
		if (playerBackgroundSpecialResult == 5 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Healer";}
		if (playerBackgroundSpecialResult == 6 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Quartermaster";}
		if (playerBackgroundSpecialResult == 7 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Standard bearer";}
		if (playerBackgroundSpecialResult == 8 && playerBackground == "a Soldier") {playerBackgroundSpecial += "Support staff (cook, blacksmith, or the like)";}

		if (playerPersonalityResult == 1 && playerBackground == "a Soldier") {playerPersonality = "You're always polite and respectful.";}
		if (playerPersonalityResult == 2 && playerBackground == "a Soldier") {playerPersonality = "You're haunted by memories of war. You can't get the images of violence out of your mind.";}
		if (playerPersonalityResult == 3 && playerBackground == "a Soldier") {playerPersonality = "You've lost too many friends, and you're slow to make new ones.";}
		if (playerPersonalityResult == 4 && playerBackground == "a Soldier") {playerPersonality = "You're full of inspiring and cautionary tales from your military experience relevant to almost every combat situation.";}
		if (playerPersonalityResult == 5 && playerBackground == "a Soldier") {playerPersonality = "You can stare down a hell hound without flinching. ";}
		if (playerPersonalityResult == 6 && playerBackground == "a Soldier") {playerPersonality = "You enjoy being strong and like breaking things.";}
		if (playerPersonalityResult == 7 && playerBackground == "a Soldier") {playerPersonality = "You have a crude sense of humor.";}
		if (playerPersonalityResult == 8 && playerBackground == "a Soldier") {playerPersonality = "You face problems head on. A simple, direct solution is the best path to success.";}

		if (playerIdealResult == 1 && playerBackground == "a Soldier") {playerIdeal = "Greater Good. Your lot is to lay down your life in defense of others. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "a Soldier") {playerIdeal = "Responsibility. You do what you must and obey just authority. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "a Soldier") {playerIdeal = "Independence. When people follow orders blindly, they embrace a kind of tyranny. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "a Soldier") {playerIdeal = "Might. In life as in war, the stronger force wins. (Evil) ";}
		if (playerIdealResult == 5 && playerBackground == "a Soldier") {playerIdeal = "Live and Let Live. Ideals aren't worth killing over or going to war for. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "a Soldier") {playerIdeal = "Nation. Your city, nation, or people are all that matter. (Any alignment)";}

		if (playerBondResult == 1 && playerBackground == "a Soldier") {playerBond = "You would still lay down your life for the people you served with.";}
		if (playerBondResult == 2 && playerBackground == "a Soldier") {playerBond = "Someone saved your life on the battlefield. To this day, you will never leave a friend behind.";}
		if (playerBondResult == 3 && playerBackground == "a Soldier") {playerBond = "Your honor is your life.";}
		if (playerBondResult == 4 && playerBackground == "a Soldier") {playerBond = "You'll never forget the crushing defeat your company suffered or the enemies who dealt it.";}
		if (playerBondResult == 5 && playerBackground == "a Soldier") {playerBond = "Those who fight beside you are those worth dying for.";}
		if (playerBondResult == 6 && playerBackground == "a Soldier") {playerBond = "You fight for those who cannot fight for themselves.";}

		if (playerFlawResult == 1 && playerBackground == "a Soldier") {playerFlaw = "The monstrous enemy you faced in battle still leaves you quivering with fear.";}
		if (playerFlawResult == 2 && playerBackground == "a Soldier") {playerFlaw = "You have little respect for anyone who is not a proven warrior.";}
		if (playerFlawResult == 3 && playerBackground == "a Soldier") {playerFlaw = "You made a terrible mistake in battle that cost many lives--and you would do anything to keep that mistake secret.";}
		if (playerFlawResult == 4 && playerBackground == "a Soldier") {playerFlaw = "Your hatred of your enemies is blind and unreasoning. ";}
		if (playerFlawResult == 5 && playerBackground == "a Soldier") {playerFlaw = "You obey the law, even if the law causes misery.";}
		if (playerFlawResult == 6 && playerBackground == "a Soldier") {playerFlaw = "You'd rather eat your armor than admit when you're wrong.";}

		if (backgroundResult == 13) {playerBackground = "an Urchin";}
		if (playerBackground == "an Urchin") {playerBackgroundSpecial = null;}

		if (playerPersonalityResult == 1 && playerBackground == "an Urchin") {playerPersonality = "You hide scraps of food and trinkets away in your pockets.";}
		if (playerPersonalityResult == 2 && playerBackground == "an Urchin") {playerPersonality = "You ask a lot of questions.";}
		if (playerPersonalityResult == 3 && playerBackground == "an Urchin") {playerPersonality = "You like to squeeze into small places where no one else can get to you.";}
		if (playerPersonalityResult == 4 && playerBackground == "an Urchin") {playerPersonality = "You sleep with your back to a wall or tree, with everything you own wrapped in a bundle in your arms.";}
		if (playerPersonalityResult == 5 && playerBackground == "an Urchin") {playerPersonality = "You eat like a pig and have bad manners.";}
		if (playerPersonalityResult == 6 && playerBackground == "an Urchin") {playerPersonality = "You think anyone who's nice to you is hiding evil intent.";}
		if (playerPersonalityResult == 7 && playerBackground == "an Urchin") {playerPersonality = "You don't like to bathe.";}
		if (playerPersonalityResult == 8 && playerBackground == "an Urchin") {playerPersonality = "You bluntly say what other people are hinting at or hiding.";}

		if (playerIdealResult == 1 && playerBackground == "an Urchin") {playerIdeal = "Respect. All people, rich or poor, deserve respect. (Good)";}
		if (playerIdealResult == 2 && playerBackground == "an Urchin") {playerIdeal = "Community. We have to take care of each other, because no one else is going to do it. (Lawful)";}
		if (playerIdealResult == 3 && playerBackground == "an Urchin") {playerIdeal = "Change. The low are lifted up, and the high and mighty are brought down. Change is the nature of things. (Chaotic)";}
		if (playerIdealResult == 4 && playerBackground == "an Urchin") {playerIdeal = "Retribution. The rich need to be shown what life and death are like in the gutters. (Evil)";}
		if (playerIdealResult == 5 && playerBackground == "an Urchin") {playerIdeal = "People. You help the people who help you--that's what keeps you all alive. (Neutral)";}
		if (playerIdealResult == 6 && playerBackground == "an Urchin") {playerIdeal = "Aspiration. You're going to prove that you're worthy of a better life.";}

		if (playerBondResult == 1 && playerBackground == "an Urchin") {playerBond = "Your town or city is your home, and you'll fight to defend it.";}
		if (playerBondResult == 2 && playerBackground == "an Urchin") {playerBond = "You sponsor an orphanage to keep others from enduring what you were forced to endure.";}
		if (playerBondResult == 3 && playerBackground == "an Urchin") {playerBond = "You owe your survival to another urchin who taught you to live on the streets.";}
		if (playerBondResult == 4 && playerBackground == "an Urchin") {playerBond = "You owe a debt you can never repay to the person who took pity on you.";}
		if (playerBondResult == 5 && playerBackground == "an Urchin") {playerBond = "You escaped your life of poverty by robbing an important person, and you're wanted for it.";}
		if (playerBondResult == 6 && playerBackground == "an Urchin") {playerBond = "No one else should have to endure the hardships you've been through.";}

		if (playerFlawResult == 1 && playerBackground == "an Urchin") {playerFlaw = "If you're outnumbered, you will run away from a fight.";}
		if (playerFlawResult == 2 && playerBackground == "an Urchin") {playerFlaw = "Gold seems like a lot of money to you, and you'll do just about anything for more of it.";}
		if (playerFlawResult == 3 && playerBackground == "an Urchin") {playerFlaw = "You will never fully trust anyone other than yourself.";}
		if (playerFlawResult == 4 && playerBackground == "an Urchin") {playerFlaw = "You'd rather kill someone in their sleep then fight fair.";}
		if (playerFlawResult == 5 && playerBackground == "an Urchin") {playerFlaw = "It's not stealing if you need it more than someone else.";}
		if (playerFlawResult == 6 && playerBackground == "an Urchin") {playerFlaw = "People who can't take care of themselves get what they deserve.";}

		return playerBackground;
	}

	void GenerateBasics () {

		//roll gender
		RollGender();

		//roll race, subrace
		RollRace();

		//roll class, subclass
		RollClass();

		//roll background (including variants)
		RollBackground();

	}

	string RollBirthplace () {
		int birthplaceResult = roll (1, 100);
		if (birthplaceResult >= 1 && birthplaceResult <= 50) {playerBirthplace = "at home.";}
		if (birthplaceResult >= 51 && birthplaceResult <= 55) {playerBirthplace = "at the home of a family friend.";}
		if (birthplaceResult >= 56 && birthplaceResult <= 63) {playerBirthplace = "at the home of a healer or midwife.";}
		if (birthplaceResult >= 64 && birthplaceResult <= 65) {playerBirthplace = "in a carriage, cart, or wagon.";}
		if (birthplaceResult >= 66 && birthplaceResult <= 68) {playerBirthplace = "in a barn, shed, or other outbuilding.";}
		if (birthplaceResult >= 69 && birthplaceResult <= 70) {playerBirthplace = "in a cave.";}
		if (birthplaceResult >= 71 && birthplaceResult <= 72) {playerBirthplace = "in a field.";}
		if (birthplaceResult >= 73 && birthplaceResult <= 74) {playerBirthplace = "in a forest.";}
		if (birthplaceResult >= 75 && birthplaceResult <= 77) {playerBirthplace = "in a temple.";}
		if (birthplaceResult == 78) {playerBirthplace = "on a battlefield.";}
		if (birthplaceResult >= 79 && birthplaceResult <= 80) {playerBirthplace = "in an alley or on the street.";}
		if (birthplaceResult >= 81 && birthplaceResult <= 82) {playerBirthplace = "in a brothel, tavern, or inn.";}
		if (birthplaceResult >= 83 && birthplaceResult <= 84) {playerBirthplace = "in a castle, keep, tower, or palace.";}
		if (birthplaceResult == 85) {playerBirthplace = "in a sewer or rubbish heap.";}
		if (birthplaceResult >= 86 && birthplaceResult <= 88) {playerBirthplace = "among people of a different race.";}
		if (birthplaceResult >= 89 && birthplaceResult <= 91) {playerBirthplace = "on board a boat or ship.";}
		if (birthplaceResult >= 92 && birthplaceResult <= 93) {playerBirthplace = "in a prison or in the headquarters of a secret organization.";}
		if (birthplaceResult >= 94 && birthplaceResult <= 95) {playerBirthplace = "in a sage's laboratory.";}
		if (birthplaceResult == 96) {playerBirthplace = "in the Feywild.";}
		if (birthplaceResult == 97) {playerBirthplace = "in the Shadowfell.";}
		if (birthplaceResult == 98 && flip == 1) {playerBirthplace = "on the Astral Plane.";}
		if (birthplaceResult == 98 && flip == 2) {playerBirthplace = "on the Ethereal Plane.";}
		if (birthplaceResult == 99) {playerBirthplace = "on one of the Inner Planes.";}
		if (birthplaceResult == 100) {playerBirthplace = "on one of the Outer Planes.";}

		//Check for strange event (1% chance)
		int strangeBirthEventResult = roll (1, 100);
		if (strangeBirthEventResult == 100) {playerBirthplace += " An exceedingly strange event marked the moment of your birth.";}

		return playerBirthplace;
	}

	string RollBirthOrder() {
		int birthOrderResult = roll (2, 6);
		if (birthOrderResult == 2) {birthOrder = "Twin/triplet/quadruplet";}
		if (birthOrderResult >= 3 && birthOrderResult <= 7) {birthOrder = "Older";}
		if (birthOrderResult >= 7 && birthOrderResult <= 12) {birthOrder = "Younger";}
		return birthOrder;
	}

	string RollSiblings() {
		//Roll # of siblings (-2 if elf or dwarf)
		int numberOfSiblingsResult = roll (1, 10);
		if (playerRace == "Hill Dwarf" || playerRace == "Mountain Dwarf" || playerRace == "Gray Dwarf" || playerRace == "High Elf" || playerRace == "Wood Elf" || playerRace == "Dark Elf" || playerRace == "Eladrin") {numberOfSiblingsResult -= 2;}
		if (numberOfSiblingsResult <= 2) {playerNumberOfSiblings = 0;}
		if (numberOfSiblingsResult >= 3 && numberOfSiblingsResult <= 4) {playerNumberOfSiblings = roll(1,3);}
		if (numberOfSiblingsResult >= 5 && numberOfSiblingsResult <= 6) {playerNumberOfSiblings = roll(1,4) + 1;}
		if (numberOfSiblingsResult >= 7 && numberOfSiblingsResult <= 8) {playerNumberOfSiblings = roll(1,6) + 2;}
		if (numberOfSiblingsResult >= 9 && numberOfSiblingsResult <= 10) {playerNumberOfSiblings = roll(1,8) + 3;}

		//Determine birth order, gender, alignment, occupation, relationship, status for each sibling
		int playerNumberOfSiblingsCounter = playerNumberOfSiblings;
		for (int i = 1; i <= playerNumberOfSiblingsCounter; i++) 
		{
			siblingText += "Sibling #" + i + ": " + RollBirthOrder() + ", " + RollGender () + ", " + RollAlignment () + ", and " + RollOccupation () + ". " +
				"You had a " + RollRelationship () + " relationship; they're currently " + RollNPCStatus () + ".\n";
		}

		if (playerNumberOfSiblings == 1) {grammarRollSiblings = "You have " + playerNumberOfSiblings + " sibling.\n" + siblingText;}
		if (playerNumberOfSiblings != 1) {grammarRollSiblings = "You have " + playerNumberOfSiblings + " siblings.\n" + siblingText;}

		return grammarRollSiblings;
	}

	string RollFamily() {
		int familyResult = roll (1, 100);
		if (familyResult == 1) {playerFamily = "by nobody at all because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult == 2) {playerFamily = "in an institution, such as an asylum, because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult == 3) {playerFamily = "in a temple because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 4 && familyResult <= 5) {playerFamily = "in an orphanage because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 6 && familyResult <= 7) {playerFamily = "by a guardian because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 8 && familyResult <= 15 && flip == 1) {playerFamily = "by your aunt and/or uncle because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 8 && familyResult <= 15 && flip == 2) {playerFamily = "by your extended family, tribe, or clan because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 16 && familyResult <= 25 && flip == 1) {playerFamily = "by your paternal grandparents because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 16 && familyResult <= 25 && flip == 2) {playerFamily = "by your maternal grandparents because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 26 && familyResult <= 35 && flip == 1) {playerFamily = "by an adoptive family of the same race because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 26 && familyResult <= 35 && flip == 2) {playerFamily = "by an adoptive family of a different race because your mother " + AbsentParent() + " and your father " + AbsentParent() + ".";}
		if (familyResult >= 36 && familyResult <= 55 && flip == 1) {playerFamily = "by a single father because your mother " + AbsentParent() + ".";}
		if (familyResult >= 36 && familyResult <= 55 && flip == 2) {playerFamily = "by a single stepfather because your mother " + AbsentParent() + ".";}
		if (familyResult >= 56 && familyResult <= 75 && flip == 1) {playerFamily = "by a single mother because your father " + AbsentParent() + ".";}
		if (familyResult >= 56 && familyResult <= 75 && flip == 2) {playerFamily = "by a single stepmother because your father " + AbsentParent() + ".";}
		if (familyResult >= 76 && familyResult <= 100) {playerFamily = "by your mother and father. You were " + RollRelationship () + " toward your mother, and she is currently " + RollNPCStatus() + ". You were " + RollRelationship () + " toward your father, and he is currently " + RollNPCStatus() + ".";}
		return playerFamily;
	}

	string AbsentParent(){
		int absentParentResult = roll (1, 4);
		if (absentParentResult == 1) {absentParent = "died" + CauseOfDeath();}
		if (absentParentResult == 2) {absentParent = "was imprisoned, enslaved, or otherwise taken away";}
		if (absentParentResult == 3) {absentParent = "abandoned you";}
		if (absentParentResult == 4) {absentParent = "disappeared to an unknown fate";}
		return absentParent;
	}

	string RollLifestyle() {
		int lifestyleResult = roll (3, 6);
		if (lifestyleResult == 3) {playerLifestyle = "miserably wretched"; childhoodHomeModifier = -40;}
		if (lifestyleResult >= 4 && lifestyleResult <= 5) {playerLifestyle = "downright squalid"; childhoodHomeModifier = -20;}
		if (lifestyleResult >= 6 && lifestyleResult <= 8) {playerLifestyle = "quite poor"; childhoodHomeModifier = -10;}
		if (lifestyleResult >= 9 && lifestyleResult <= 12) {playerLifestyle = "modest"; childhoodHomeModifier = 0;}
		if (lifestyleResult >= 13 && lifestyleResult <= 15) {playerLifestyle = "pleasantly comfortable"; childhoodHomeModifier = +10;}
		if (lifestyleResult >= 16 && lifestyleResult <= 17) {playerLifestyle = "rather wealthy"; childhoodHomeModifier = +20;}
		if (lifestyleResult == 18) {playerLifestyle = "exquisitely aristocratic"; childhoodHomeModifier = +40;}
		return playerLifestyle;
	}

	string RollHome() {
		int playerHomeResult = roll (1, 100) + childhoodHomeModifier;
		if (playerHomeResult <= 0) {playerHome = "on the streets.";}
		if (playerHomeResult >= 1 && playerHomeResult <= 20) {playerHome = "in a rundown shack.";}
		if (playerHomeResult >= 21 && playerHomeResult <= 30) {playerHome = "in no permanent residence; you moved around frequently.";}
		if (playerHomeResult >= 31 && playerHomeResult <= 40) {playerHome = "in an encampment or village in the wilderness.";}
		if (playerHomeResult >= 41 && playerHomeResult <= 50) {playerHome = "in an apartment in a rundown neighborhood.";}
		if (playerHomeResult >= 51 && playerHomeResult <= 70) {playerHome = "in a small house.";}
		if (playerHomeResult >= 71 && playerHomeResult <= 90) {playerHome = "in a large house.";}
		if (playerHomeResult >= 91 && playerHomeResult <= 110) {playerHome = "in a spacious mansion.";}
		if (playerHomeResult >= 111) {playerHome = "in an enormous palace or castle.";}
		return playerHome;
	}

	string RollChildhoodMemories() {
		int childhoodMemoriesResult = roll (3, 6) + childhoodMemoriesModifier;
		if (childhoodMemoriesResult <= 3) {playerChildhoodMemories = "You are still haunted by your childhood, when you were treated badly by your peers.";}
		if (childhoodMemoriesResult >= 4 && childhoodMemoriesResult <= 5) {playerChildhoodMemories = "You spent most of your childhood alone, with no close friends.";}
		if (childhoodMemoriesResult >= 6 && childhoodMemoriesResult <= 8) {playerChildhoodMemories = "Others saw you as being different or strange, and so you had few companions.";}
		if (childhoodMemoriesResult >= 9 && childhoodMemoriesResult <= 12) {playerChildhoodMemories = "You had a few close friends and lived an ordinary childhood.";}
		if (childhoodMemoriesResult >= 13 && childhoodMemoriesResult <= 15) {playerChildhoodMemories = "You had several friends, and your childhood was generally a happy one.";}
		if (childhoodMemoriesResult >= 16 && childhoodMemoriesResult <= 17) {playerChildhoodMemories = "You always found it easy to make friends, and you loved being around people.";}
		if (childhoodMemoriesResult >= 18) {playerChildhoodMemories = "Everyone knew who you were, and you had friends everywhere you went.";}
		return playerChildhoodMemories;
	}

	void GenerateOrigins () {
		//roll parents, roll racial-specific parent, if necessary

		int parentsResult = roll (1, 100);
		if (parentsResult <= 95) {
			parentsKnown = true;
			playerParentPresence = "You know who your parents are or were.";
		}
		if (parentsResult >= 96) {
			playerParentPresence = "You do not know who your parents were.";
		}

		if (parentsKnown == true) {
			if (playerRace == "Half-elf" || playerRace == "Half-Wood Elf" || playerRace == "Half-Sun/Moon Elf" || playerRace == "Half-Dark Elf" || playerRace == "Half-Aquatic Elf") {
				int mixedAncestryResultsHalfElf = roll (1, 8);
				if (mixedAncestryResultsHalfElf <= 5) {
					mixedAncestry = "One parent was an elf and the other was a human. ";
				}
				if (mixedAncestryResultsHalfElf == 6) {
					mixedAncestry = "One parent was an elf and the other was a half-elf. ";
				}
				if (mixedAncestryResultsHalfElf == 7) {
					mixedAncestry = "One parent was a human and the other was a half-elf. ";
				}
				if (mixedAncestryResultsHalfElf == 8) {
					mixedAncestry = "Both parents were half-elves. ";
				}
			}

			if (playerRace == "Half-orc") {
				int mixedAncestryResultsHalfOrc = roll (1, 8);
				if (mixedAncestryResultsHalfOrc <= 3) {
					mixedAncestry = "One parent was an orc and the other was a human. ";
				}
				if (mixedAncestryResultsHalfOrc >= 4 && mixedAncestryResultsHalfOrc <= 5) {
					mixedAncestry = "One parent was an orc and the other was a half-orc. ";
				}
				if (mixedAncestryResultsHalfOrc >= 6 && mixedAncestryResultsHalfOrc <= 7) {
					mixedAncestry = "One parent was a human and the other was a half-orc. ";
				}
				if (mixedAncestryResultsHalfOrc == 8) {
					mixedAncestry = "Both parents were half-orcs. ";
				}
			}

			if (playerRace == "Tiefling") {
				int mixedAncestryResultsTiefling = roll (1, 8);
				if (mixedAncestryResultsTiefling <= 4) {
					mixedAncestry = "Both parents were humans, their infernal heritage dormant until you came along. ";
				}
				if (mixedAncestryResultsTiefling >= 5 && mixedAncestryResultsTiefling <= 6) {
					mixedAncestry = "One parent was a tiefling and the other was a human. ";
				}
				if (mixedAncestryResultsTiefling == 7) {
					mixedAncestry = "One parent was a tiefling and the other was a devil. ";
				}
				if (mixedAncestryResultsTiefling == 8) {
					mixedAncestry = "One parent was a human and the other was a devil. ";
				}
			}

			//Roll mother's alignment, occupation, relationship, status
			playerMotherDetails = "Your mother was " + RollAlignment () + " and spent her days as " + RollOccupation () + ".";
				// + "You were " + RollRelationship () + " toward each other. Your mother is " + RollNPCStatus() + "."; 


			//Roll father's alignment, occupation, relationship, status
			playerFatherDetails = "Your father was " + RollAlignment() + " and spent his days as " + RollOccupation() + ".";
			 	// + "You were " + RollRelationship () + " toward each other. Your father is " + RollNPCStatus() + ".";
		}
			
		//Roll birthplace
		RollBirthplace();


		//Roll # of siblings, birth order, gender, alignment, occupation, relationship, status for each sibling
		//TODO: This is done up top; eventually clean up the functions so that they all either execute internally or all at the top.

		//Roll family table -> Absent parent if necessary (Cause of Death?)
		RollFamily();

		//Roll family lifestyle; note Childhood Home modifier
		RollLifestyle();

		//Roll childhood home + modifier
		RollHome();

		//Determine if rolled class has Charisma as a key stat or rolled race offers Charisma bonus. If yes, cumulative +2 (class) or +1 (race) to Childhood Memories roll.
		//This is an abstraction.
		//Roll childhood memories + CHA modifier
		RollChildhoodMemories();
	}


	string RollBackgroundPersonalDecision() {
		int backgroundPersonalDecisionResult = roll (1, 6);
		if (playerBackground == "an Acolyte") 
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You ran away form home at an early age and found refuge in a temple.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "Your family gave you to a temple, since they were unable or unwilling to care for me.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You grew up in a household with strong religious convictions. Entering the service of one or more gods seemed natural.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "An impassioned sermon struck a chord deep in your soul and moved you to serve the faith.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You followed a childhood friend, a respected acquaintance, or someone you loved into religious service.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "After encountering a true servant of the gods, you were so inspired that you immediately entered the service of a religious group.";}
		}

		if (playerBackground == "a Charlatan") 
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You were left to your own devices, and your knack for manipulating others helped you survive.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You learned early on that people were gullible and easy to exploit.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You often got in trouble, but you managed to talk your way out of it every time.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You took up with a confidence artist, from whom you learned your craft.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "After a charlatan fleeced your family, you decided to learn the trade so you would never be fooled by such deception again";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You were poor or feared becoming poor, so you learned the tricks needed to keep yourself out of poverty.";}
		}

		if (playerBackground == "a Spy" || playerBackground == "a Criminal")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You resented authority in your younger days and saw a life of crime as the best way to fight against tyranny and oppression.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "Necessity forced you to take up the life, since it was the only way you could survive.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You fell in with a gang of reprobates and ne'er-do-wells, and you learned your speciality from them.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "A parent or relative taught you your criminal speciality to prepare you for the family business.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You left home and found a place in a thieves' guild or some other criminal organization.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You were always bored, so you turned to crime to pass the time and discovered you were quite good at it.";}
		}

		if (playerBackground == "an Entertainer" || playerBackground == "a Gladiator")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "Members of your family made ends meet by performing, so it was fitting for you to follow their example.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You always had a keen insight into other people, enough so that you could make them laugh or cry with your stories or songs.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You ran away from home to follow a minstrel troupe.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You saw a bard perform once, and you knew from that moment on what you were born to do.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You earned coin by performing on street corners and eventually made a name for yourself.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "A traveling entertainer took you in and taught you the trade.";}
		}

		if (playerBackground == "a Folk Hero")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You learned what was right and wrong frmo your family.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You were always enamored by tales of heroes and wished you could be something more than ordinary.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You hated your mundane life, so when it was time for someone to step up and do the right thing, you took your chance.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "A parent or one of your relatives was an adventurer, and you were inspired by that person’s courage.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "A mad old hermit spoke a prophecy when you was born, saying that you would accomplish great things.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You have always stood up for those who are weaker than you are.";}
		}

		if (playerBackground == "a Guild Artisan" || playerBackground == "a Guild Merchant")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You were apprenticed to a master who taught you the guild’s business.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You helped a guild artisan keep a secret or complete a task, and in return you were taken on as an apprentice.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "One of your family members who belonged to the guild made a place for you.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You were always good with your hands, so you took the opportunity to learn a trade.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You wanted to get away from your home situation and start a new life.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You learned the essentials of your craft from a mentor but had to join the guild to finish your training.";}
		}

		if (playerBackground == "a Hermit")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "Your enemies ruined your reputation, and you fled to the wilds to avoid further disparagement.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You are comfortable with being isolated, as you seek inner peace.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You never liked the people you called your friends, so it was easy for you to strike out on your own.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You felt compelled to forsake your past, but did so with great reluctance, and sometimes you regret making that decision.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You lost everything — your home, your family, your friends. Going it alone was all you could do.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "Society’s decadence disgusted you, so you decided to leave it behind.";}
		}

		if (playerBackground == "a Noble" || playerBackground == "a Knight")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You come from an old and storied family, and it fell to you to preserve the family name.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "Your family has been disgraced, and you intend to clear your name.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "Your family recently came by its title, and that elevation thrust you into a new and strange world.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "Your family has a title, but none of your ancestors have distinguished themselves since they gained it.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "Your family is filled with remarkable people. You hope to live up to their example.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You hope to increase your family’s power and influence.";}
		}

		if (playerBackground == "an Outlander")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You spent a lot of time in the wilderness as a youngster, and you came to love that way of life.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "From a young age,  couldn’t abide the stink of the cities and preferred to spend your time in nature.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You came to understand the darkness that lurks in the wilds, and you vowed to combat it.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "Your people lived on the edges of civilization, and you learned the methods of survival from your family.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "After a tragedy you retreated to the wilderness, leaving your old life behind.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "Your family moved away from civilization, and you learned to adapt to your new environment.";}
		}

		if (playerBackground == "a Sage")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You were naturally curious, so you packed up and went to a university to learn more about the world.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "Your mentor’s teachings opened your mind to new possibilities in that field of study.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "You were always an avid reader, and you learned much about your favorite topic on your own.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You discovered an old library and pored over the texts you found there. That experience awakened a hunger for more knowledge.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You impressed a wizard who told you you were squandering your talents and should seek out an education to take advantage of your gifts.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "One of your parents or a relative gave you a basic education that whetted your appetite, and you left home to build on what you had learned.";}
		}

		if (playerBackground == "a Sailor" || playerBackground == "a Pirate")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You were press-ganged by pirates and forced to serve on their ship until you finally escaped.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You wanted to see the world, so you signed on as a deckhand for a merchant ship.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "One of your relatives was a sailor who took you to sea.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "You needed to escape your community quickly, so you stowed away on a ship. When the crew found you, you were forced to work for your passage.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "Reavers attacked your community, so you found refuge on a ship until you could seek vengeance.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "You had few prospects where you were living, so you left to find your fortune elsewhere.";}
		}

		if (playerBackground == "a Soldier")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "You joined the militia to help protect your community from monsters.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "A relative of yours was a soldier, and you wanted to carry on the family tradition.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "The local lord forced you to enlist in the army.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "War ravaged your homeland while you were growing up. Fighting was the only life you ever knew.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "You wanted fame and fortune, so you joined a mercenary company, selling your sword to the highest bidder.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "Invaders attacked your homeland. It was your duty to take up arms in defense of your people.";}
		}

		if (playerBackground == "an Urchin")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "Wanderlust caused you to leave your family to see the world. You look after yourself.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "You ran away from a bad situation at home and made your own way in the world.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "Monsters wiped out your village, and you were the sole survivor. You had to find a way to survive.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "A notorious thief looked after you and other orphans, and you spied and stole to earn your keep.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "One day you woke up on the streets, alone and hungry, with no memory of your early childhood.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "Your parents died, leaving no one to look after you. You raised yourself.";}
		}
		
		return "You became a(n) " + playerBackground + "because " + playerBackgroundPersonalDecision;
	}

	string RollClassPersonalDecision ()
	{
		int classPersonalDecisionResult = roll (1, 6);
		if (playerClass == "Barbarian") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "your devotion to your people lifted you in battle, making you powerful and dangerous.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "the spirits of your ancestors called on you to carry out a great task.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you lost control in battle one day, and it was as if something else was manipulating your body, forcing it to kill every foe you could reach.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you went on a spiritual journey to find yourself and instead found a spirit animal to guide, protect, and inspire you.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you were struck by lightning and lived. Afterward, you found a new strength within you that let you push beyond your limitations.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "your anger needed to be channeled into battle, or you risked becoming an indiscriminate killer.";}
		}

		if (playerClass == "Bard") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you awakened your latent bardic abilities through trial and error.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you were a gifted performer and attracted the attention of a master bard who schooled you in the old techniques.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you joined a loose society of scholars and orators to learn new techniques of performance and magic.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you felt a calling to recount the deeds of champions and heroes, to bring them alive in song and story.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you joined one of the great colleges to learn old lore, the secrets of magic, and the art of performance.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you picked up a musical instrument one day and instantly discovered that you could play it.";}
		}

		if (playerClass == "Cleric") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "a supernatural being in service to the gods called you to become a divine agent in the world.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you saw the injustice and horror in the world and felt moved to take a stand against them.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "your god gave you an unmistakable sign. You dropped everything to serve the divine.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "although you were always devout, it wasn’t until you completed a pilgrimage that you knew your true calling.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you used to serve in your religion’s bureaucracy but found you needed to work in the world, to bring the message of your faith to the darkest corners of the land.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you realize that your god works through you, and you do as commanded, even though you don’t know why you were chosen to serve.";}
		}

		if (playerClass == "Druid") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you saw too much devastation in the wild places, too much of nature’s splendor ruined by the despoilers. You joined a circle of druids to fight back against the enemies of nature.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you found a place among a group of druids after you fled a catastrophe.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you have always had an affinity for animals, so you explored my talent to see how you could best use it.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you befriended a druid and were moved by druidic teachings. You decided to follow your friend’s guidance and give something back to the world.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "while you were growing up, you saw spirits all around you — entities no one else could perceive. You sought out the druids to help you understand the visions and communicate with these beings.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you have always felt disgust for creatures of unnatural origin. For this reason, you immersed yourself in the study of the druidic mysteries and became a champion of the natural order.";}
		}

		if (playerClass == "Fighter") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you wanted to hone your combat skills, and so you joined a war college.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you squired for a knight who taught you how to fight, care for a steed, and conduct yourself with honor. You decided to take up that path for yourself.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "horrible monsters descended on your community, killing someone you loved. You took up arms to destroy those creatures and others of a similar nature.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you joined the army and learned how to fight as part of a group.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you grew up fighting, and you refined your talents by defending yourself against people who crossed you.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you could always pick up just about any weapon and know how to use it effectively.";}
		}

		if (playerClass == "Monk") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you were chosen to study at a secluded monastery. There, you were taught the fundamental techniques required to eventually master a tradition.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you sought instruction to gain a deeper understanding of existence and your place in the world.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you stumbled into a portal to the Shadowfell and took refuge in a strange monastery, where you learned how to defend yourself against the forces of darkness.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you were overwhelmed with grief after losing someone close to you, and you sought the advice of philosophers to help you cope with your loss.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you could feel that a special sort of power lay within you, so you sought out those who could help you call it forth and master it.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you were wild and undisciplined as a youngster, but then you realized the error of your ways. You applied to a monastery and became a monk as a way to live a life of discipline.";}
		}

		if (playerClass == "Paladin") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "a fantastical being appeared before you and called on you to undertake a holy quest.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "one of your ancestors left a holy quest unfulfilled, so you intend to finish that work.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "the world is a dark and terrible place. You decided to serve as a beacon of light shining out against the gathering shadows.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you served as a paladin’s squire, learning all you needed to swear your own sacred oath.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "evil must be opposed on all fronts. You feel compelled to seek out wickedness and purge it from the world.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "becoming a paladin was a natural consequence of your unwavering faith. In taking your vows, you became the holy sword of your religion.";}
		}

		if (playerClass == "Ranger") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you found purpose while you honed your hunting skills by bringing down dangerous animals at the edge of civilization.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you always had a way with animals, able to calm them with a soothing word and a touch.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you suffer from terrible wanderlust, so being a ranger gave you a reason not to remain in one place for too long.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you have seen what happens when the monsters come out from the dark. You took it upon yourself to become the first line of defense against the evils that lie beyond civilization’s borders.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you met a grizzled ranger who taught you woodcraft and the secrets of the wild lands.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you served in an army, learning the precepts of your profession while blazing trails and scouting enemy encampments.";}
		}

		if (playerClass == "Rogue") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "you’ve always been nimble and quick of wit, so you decided to use those talents to help yourself make your way in the world.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "an assassin or a thief wronged you, so you focused your training on mastering the skills of your enemy to better combat foes of that sort.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "an experienced rogue saw something in you and taught you several useful tricks.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "you decided to turn your natural lucky streak into the basis of a career, though you still realize that improving your skills is essential.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "you took up with a group of ruffians who showed you how to get what you want through sneakiness rather than direct confrontation.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you're a sucker for a shiny bauble or a sack of coins, as long as you can get your hands on it without risking life and limb.";}
		}

		if (playerClass == "Sorcerer") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "when you were born, all the water in the house froze solid, the milk spoiled, or all the iron turned to copper. Your family is convinced that this event was a harbinger of stranger things to come for you.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you suffered a terrible emotional or physical strain, which brought forth your latent magical power. You have fought to control it ever since.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "your immediate family never spoke of your ancestors, and when you asked, they would change the subject. It wasn’t until you started displaying strange talents that the full truth of your heritage came out.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "when a monster threatened one of your friends, you became filled with anxiety. You lashed out instinctively and blasted the wretched thing with a force that came from within you.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "sensing something special in you, a stranger taught you how to control my gift.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "after you escaped from a magical conflagration, you realized that though you were unharmed, you were not unchanged. You began to exhibit unusual abilities that you are just beginning to understand.";}
		}

		if (playerClass == "Warlock") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "while wandering around in a forbidden place, you encountered an otherworldly being that offered to enter into a pact with you.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "you were examining a strange tome you found in an abandoned library when the entity that would become your patron suddenly appeared before you.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you stumbled into the clutches of your patron after you accidentally stepped through a magical doorway.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "when you were faced with a terrible crisis, you prayed to any being who would listen, and the creature that answered became your patron.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "your future patron visited you in your dreams and offered great power in exchange for your service.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "one of your ancestors had a pact with your patron, so that entity was determined to bind you to the same agreement.";}
		}

		if (playerClass == "Wizard") 
		{
			if (classPersonalDecisionResult == 1) {playerClassPersonalDecision = "an old wizard chose you from among several candidates to serve an apprenticeship.";}
			if (classPersonalDecisionResult == 2) {playerClassPersonalDecision = "when you became lost in a forest, a hedge wizard found you, took you in, and taught you the rudiments of magic.";}
			if (classPersonalDecisionResult == 3) {playerClassPersonalDecision = "you grew up listening to tales of great wizards and knew you wanted to follow their path. You strove to be accepted at an academy of magic and succeeded.";}
			if (classPersonalDecisionResult == 4) {playerClassPersonalDecision = "one of your relatives was an accomplished wizard who decided you were smart enough to learn the craft.";}
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "while exploring an old tomb, library, or temple, you found a spellbook. You were immediately driven to learn all you could about becoming a wizard.";}
			if (classPersonalDecisionResult == 6) {playerClassPersonalDecision = "you were a prodigy who demonstrated mastery of the arcane arts at an early age. When you became old enough to set out on your own, you did so to learn more magic and expand your power.";}
		}

		return "You became a(n) " + playerBackground + "because " + playerBackgroundPersonalDecision;
	}

	void GeneratePersonalDecisions () {

		//Roll Background details
		RollBackgroundPersonalDecision();

		//Roll Class Training
			//Roll specific class detail tables
		RollClassPersonalDecision();
	}

	void RollAgeAndNumberOfLifeEvents ()
	{
		int ageResult = roll (1, 100);
		if (ageResult >= 1 && ageResult <= 20) {playerAge = Random.Range(18,21); playerLifeEventCount = 1;}
		if (ageResult >= 21 && ageResult <= 59) {playerAge = Random.Range(21,31); playerLifeEventCount = roll(1,4);}
		if (ageResult >= 60 && ageResult <= 69) {playerAge = Random.Range(31,41); playerLifeEventCount = roll(1,6);}
		if (ageResult >= 70 && ageResult <= 89) {playerAge = Random.Range(41,51); playerLifeEventCount = roll(1,8);}
		if (ageResult >= 90 && ageResult <= 99) {playerAge = Random.Range(51,61); playerLifeEventCount = roll(1,10);}
		if (ageResult == 100) {playerAge = Random.Range(61,106); playerLifeEventCount = roll(1,12);}
		return;
	}

	string RollLifeEvents ()
	{
		lifeEventResult = roll(1,100);
		if (lifeEventResult >= 1 && lifeEventResult <= 10) {lifeEventText = "You suffered a tragedy. " + RollTragedies () + "\n";}
		if (lifeEventResult >= 11 && lifeEventResult <= 20) {lifeEventText = "You gained a bit of good fortune. " + RollBoons () + "\n";}
		if (lifeEventResult >= 21 && lifeEventResult <= 30) {lifeEventText = "You fell in love or got married. If you get this result more than once, you can choose to have a child instead. Work with your DM to determine the identity of your love interest.\n";}

		int lifeEventEnemyBlameResult = roll(1,2);
		if (lifeEventEnemyBlameResult == 1) {lifeEventEnemyBlame = "and you are to blame";}
		if (lifeEventEnemyBlameResult == 2) {lifeEventEnemyBlame = "but you are blameless";}

		if (lifeEventResult >= 31 && lifeEventResult <= 40) {lifeEventText = "You made an enemy of an adventurer " + lifeEventEnemyBlame + ". Your enemy is a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollNPCClass() + ".\n";}
		if (lifeEventResult >= 41 && lifeEventResult <= 50) {lifeEventText = "You made a friend of an adventurer. Your friend is a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollNPCClass() + ". Work with your DM to establish how your friendship began.\n";}
		if (lifeEventResult >= 51 && lifeEventResult <= 70) {lifeEventText = "You spent time working in a job related to your background. Start the game with an extra " + roll(2,6) + " gp.\n";}
		if (lifeEventResult >= 71 && lifeEventResult <= 75) {lifeEventText = "You met someone important. They are a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " and live as " + RollOccupation() + ". They feel " + RollRelationship() + " toward you. Work out additional details with your DM as needed to fit this character into your backstory.\n";}
		if (lifeEventResult >= 76 && lifeEventResult <= 80) {lifeEventText = "You went on an adventure. " + RollAdventure();}
		if (lifeEventResult >= 81 && lifeEventResult <= 85) {lifeEventText = "You had a supernatural experience. " + RollSupernaturalEvents() + "\n";}
		if (lifeEventResult >= 86 && lifeEventResult <= 90) {lifeEventText = "You fought in a battle. " + RollWar() + "\n";}
		if (lifeEventResult >= 91 && lifeEventResult <= 95) {lifeEventText = "You committed " + RollCrime() + " or were wrongly accused of doing so. " + RollPunishment() + "\n";}
		if (lifeEventResult >= 96 && lifeEventResult <= 99) {lifeEventText = "You encountered something magical. " + RollArcaneMatters() + "\n";}
		if (lifeEventResult == 100) {lifeEventText = "Something truly strange happened to you. " + RollWeirdStuff() + "\n";}
		return lifeEventText;
	}

	string PrintLifeEvents() 
	{
		int playerLifeEventCounter = playerLifeEventCount;
		for (int i = 1; i <= playerLifeEventCounter; i++) 
		{
			lifeEventText += "Event #" + i + ": " + RollLifeEvents();
		}

		if (playerLifeEventCount == 1) {grammarPrintLifeEvents = "You experienced " + playerLifeEventCount + " major event in your life so far: \n" + lifeEventText;}
		if (playerLifeEventCount != 1) {grammarPrintLifeEvents = "You experienced " + playerLifeEventCount + " major events in your life so far: \n" + lifeEventText;}

		return grammarPrintLifeEvents;
	}



	void GenerateLifeEvents () {

		//Roll age and # of life events
		RollAgeAndNumberOfLifeEvents ();

		//Roll each life event and life event sub-tables
		//Because this is an iterative process, THIS FUNCTION IS AT THE TOP OF THE SCRIPT, rather than down here.
	}

	//all the life event sub-tables follow this comment

	string RollAdventure ()
	{
		int AdventureResult = roll (1, 100);

		int missingBodyPartsResult = roll (1, 3);
			if (missingBodyPartsResult == 1) {missingBodyPart = "an ear";}
			if (missingBodyPartsResult == 2) {missingBodyPart = roll(1,3) + " fingers";}
			if (missingBodyPartsResult == 3) {missingBodyPart = roll(1,4) + " toes";}

		if (AdventureResult >= 1 && AdventureResult <= 10) {AdventureText = "You nearly died. You have nasty scars on your body, and you are missing " + missingBodyPart + ".\n";}
		if (AdventureResult >= 11 && AdventureResult <= 20) {AdventureText = "You suffered a grievous injury. Although the wound healed, it still pains you from time to time.\n";}
		if (AdventureResult >= 21 && AdventureResult <= 30) {AdventureText = "You were wounded, but in time you fully recovered.\n";}

		int adventureDiseaseResult = roll (1, 3);
			if (adventureDiseaseResult == 1) {adventureDisease = "a persistent cough";}
			if (adventureDiseaseResult == 2) {adventureDisease = "pockmarks on your skin";}
			if (adventureDiseaseResult == 3) {adventureDisease = "prematurely gray hair";}

		if (AdventureResult >= 31 && AdventureResult <= 40) {AdventureText = "You contracted a disease while exploring a filthy warren. You recovered from the disease, but you have " + adventureDisease + ".\n";}

		int adventurePoisonSourceResult = roll (1, 2);
			if (adventurePoisonSourceResult == 1) {adventureDisease = "trap";}
			if (adventurePoisonSourceResult == 2) {adventureDisease = "monster";}

		if (AdventureResult >= 41 && AdventureResult <= 50) {AdventureText = "You were poisoned by a " + adventurePoisonSource + " . You recovered, but the next time you must make a saving throw against poison, you make the saving throw with disadvantage.\n";}
		if (AdventureResult >= 51 && AdventureResult <= 60) {AdventureText = "You lost something of sentimental value to you during your adventure. Remove one trinket from your possessions.\n";}
		if (AdventureResult >= 61 && AdventureResult <= 70) {AdventureText = "You were terribly frightened by something you encountered and ran away, abandoning your companions to their fate.\n";}
		if (AdventureResult >= 71 && AdventureResult <= 80) {AdventureText = "You learned a great deal during your adventure. The next time you make an ability check or a saving throw, you have advantage on the roll.\n";}
		if (AdventureResult >= 81 && AdventureResult <= 90) {AdventureText = "You found some treasure on your adventure. You have " + roll(2,6) + " gp left from your share of it.\n";}
		if (AdventureResult >= 91 && AdventureResult <= 99) {AdventureText = "You found a considerable amount of treasure on your adventure. You have " + (roll(1,20) + 50) + " gp left from your share of it.\n";}
		if (AdventureResult == 100) {AdventureText = "You came across a common magic item (of the DM’s choice).\n";}
		return AdventureText;
	}

	string RollArcaneMatters ()
	{
		int arcaneMattersResult = roll (1, 10);
		if (arcaneMattersResult == 1) {ArcaneMattersText = "You were charmed or frightened by a spell.";}
		if (arcaneMattersResult == 2) {ArcaneMattersText = "You were injured by the effect of a spell.";}

		int arcaneMattersCasterResult = roll(1,5);
			if (arcaneMattersCasterResult == 1) {arcaneMattersCaster = "cleric";}
			if (arcaneMattersCasterResult == 2) {arcaneMattersCaster = "druid";}
			if (arcaneMattersCasterResult == 3) {arcaneMattersCaster = "sorcerer";}
			if (arcaneMattersCasterResult == 4) {arcaneMattersCaster = "warlock";}
			if (arcaneMattersCasterResult == 5) {arcaneMattersCaster = "wizard";}

		if (arcaneMattersResult == 3) {ArcaneMattersText = "You witnessed a powerful spell being cast by a " + arcaneMattersCaster + ".";}
		if (arcaneMattersResult == 4) {ArcaneMattersText = "You drank a potion (of the DM’s choice).";}
		if (arcaneMattersResult == 5) {ArcaneMattersText = "You found a spell scroll (of the DM’s choice) and succeeded in casting the spell it contained.";}
		if (arcaneMattersResult == 6) {ArcaneMattersText = "You were affected by teleportation magic.";}
		if (arcaneMattersResult == 7) {ArcaneMattersText = "You turned invisible for a time.";}
		if (arcaneMattersResult == 8) {ArcaneMattersText = "You identified an illusion for what it was.";}
		if (arcaneMattersResult == 9) {ArcaneMattersText = "You saw a creature being conjured by magic.";}
		if (arcaneMattersResult == 10) {ArcaneMattersText = "Your fortune was read by a diviner. One of the following predictions is true and one is certainly false: \n\t- " + RollLifeEvents () + "\n\t- " + RollLifeEvents () + "\n";}
		return ArcaneMattersText;
	}

	string RollBoons ()
	{
		int boonsResult = roll (1, 10);
		if (boonsResult == 1) {BoonsText = "A friendly wizard gave you a spell scroll containing one cantrip (of the DM’s choice).";}
		if (boonsResult == 2) {BoonsText = "You saved the life of a commoner, who now owes you a life debt. This individual accompanies you on your travels and performs mundane tasks for you, but will leave if neglected, abused, or imperiled. Your companion is a " + RollAlignment() + " " + RollGender() + " " + RollRace() + ".";}
		if (boonsResult == 3) {BoonsText = "You found a riding horse.";}
		if (boonsResult == 4) {BoonsText = "You found some money. You have " + roll(1,20) + " gp in addition to your regular starting funds.";}
		if (boonsResult == 5) {BoonsText = "A relative bequeathed you a simple weapon of your choice.";}
		if (boonsResult == 6) {BoonsText = "You found something interesting. You gain one additional trinket.";}
		if (boonsResult == 7) {BoonsText = "You once performed a service for a local temple. The next time you visit the temple, you can receive healing up to your hit point maximum.";}
		if (boonsResult == 8) {BoonsText = "A friendly alchemist gifted you with a potion of healing or a flask of acid, as you choose.";}
		if (boonsResult == 9) {BoonsText = "You found a treasure map.";}
		if (boonsResult == 10) {BoonsText = "A distant relative left you a stipend that enables you to live at the comfortable lifestyle for " + roll(1,20) + " years. If you choose to live at a higher lifestyle, you reduce the price of the lifestyle by 2 gp during that time period.";}
		return BoonsText;
	}

	string RollCrime ()
	{
		int crimeResult = roll (1, 8);
		if (crimeResult == 1) {CrimeText = "murder";}
		if (crimeResult == 2) {CrimeText = "theft";}
		if (crimeResult == 3) {CrimeText = "burglary";}
		if (crimeResult == 4) {CrimeText = "assault";}
		if (crimeResult == 5) {CrimeText = "smuggling";}
		if (crimeResult == 6) {CrimeText = "kidnapping";}
		if (crimeResult == 7) {CrimeText = "extortion";}
		if (crimeResult == 8) {CrimeText = "counterfeiting";}
		return CrimeText;
	}

	string RollPunishment ()
	{
		int punishmentResult = roll (1, 12);
		if (punishmentResult >= 1 && punishmentResult <= 3) {PunishmentText = "You did not commit the crime and were exonerated after being accused.";}
		if (punishmentResult >= 4 && punishmentResult <= 6) {PunishmentText = "You committed the crime or helped do so, but nonetheless the authorities found you not guilty.";}
		if (punishmentResult >= 7 && punishmentResult <= 8) {PunishmentText = "You were nearly caught in the act. You had to flee and are wanted in the community where the crime occurred.";}

		int punishmentImprisonmentTypeResult = roll(1,3);
			if (punishmentImprisonmentTypeResult == 1) {punishmentImprisonmentType = "in jail";}
			if (punishmentImprisonmentTypeResult == 2) {punishmentImprisonmentType = "chained to an oar";}
			if (punishmentImprisonmentTypeResult == 3) {punishmentImprisonmentType = "performing hard labor";}

		int punishmentServedTimeResult = roll(1,2);
		if (punishmentServedTimeResult == 1) {punishmentServedTime = "served a sentence of " + roll(1,4) + " years";}
		if (punishmentServedTimeResult == 2) {punishmentServedTime = "escaped after " + roll(1,4) + " years";}

		if (punishmentResult >= 9 && punishmentResult <= 12) {PunishmentText = "You were caught and convicted. You spent time " + punishmentImprisonmentType + ". You " + punishmentServedTime + ".";}
		return PunishmentText;
	}

	string RollSupernaturalEvents ()
	{
		int supernaturalEventsResult = roll (1, 100);
		if (supernaturalEventsResult >= 1 && supernaturalEventsResult <= 5) {SupernaturalEventsText = "You were ensorcelled by a fey and enslaved for " + roll(1,6) + " years before you escaped.";}
		if (supernaturalEventsResult >= 6 && supernaturalEventsResult <= 10) {SupernaturalEventsText = "You saw a demon and ran away before it could do anything to you.";}
		if (supernaturalEventsResult >= 11 && supernaturalEventsResult <= 15) {SupernaturalEventsText = "A devil tempted you. Make a DC 10 Wisdom saving throw. On a failed save, your alignment shifts one step toward evil (if it’s not evil already), and you start the game with an additional " + (roll(1,20) + 50) + " gp.";}
		if (supernaturalEventsResult >= 16 && supernaturalEventsResult <= 20) {SupernaturalEventsText = "You woke up one morning miles from your home, with no idea how you got there.";}
		if (supernaturalEventsResult >= 21 && supernaturalEventsResult <= 30) {SupernaturalEventsText = "You visited a holy site and felt the presence of the divine there.";}
		if (supernaturalEventsResult >= 31 && supernaturalEventsResult <= 40) {SupernaturalEventsText = "You witnessed a falling red star, a face appearing in the frost, or some other bizarre happening. You are certain that it was an omen of some sort.";}
		if (supernaturalEventsResult >= 41 && supernaturalEventsResult <= 50) {SupernaturalEventsText = "You escaped certain death and believe it was the intervention of a god that saved you.";}
		if (supernaturalEventsResult >= 51 && supernaturalEventsResult <= 60) {SupernaturalEventsText = "You witnessed a minor miracle.";}
		if (supernaturalEventsResult >= 61 && supernaturalEventsResult <= 70) {SupernaturalEventsText = "You explored an empty house and found it to be haunted.";}

		int supernaturalEventsPossessionResult = roll(1,6);
			if (supernaturalEventsPossessionResult == 1) {supernaturalEventsPossession = "a celestial";}
			if (supernaturalEventsPossessionResult == 2) {supernaturalEventsPossession = "a devil";}
			if (supernaturalEventsPossessionResult == 3) {supernaturalEventsPossession = "a demon";}
			if (supernaturalEventsPossessionResult == 4) {supernaturalEventsPossession = "a fey";}
			if (supernaturalEventsPossessionResult == 5) {supernaturalEventsPossession = "an elemental";}
			if (supernaturalEventsPossessionResult == 6) {supernaturalEventsPossession = "an undead";}

		if (supernaturalEventsResult >= 71 && supernaturalEventsResult <= 75) {SupernaturalEventsText = "You were briefly possessed by " + supernaturalEventsPossession + ".";}
		if (supernaturalEventsResult >= 76 && supernaturalEventsResult <= 80) {SupernaturalEventsText = "You saw a ghost.";}
		if (supernaturalEventsResult >= 81 && supernaturalEventsResult <= 85) {SupernaturalEventsText = "You saw a ghoul feeding on a corpse.";}

		int supernaturalEventsDreamVisitResult = roll(1,2);
			if (supernaturalEventsDreamVisitResult == 1) {supernaturalEventsDreamVisit = "celestial";}
			if (supernaturalEventsDreamVisitResult == 2) {supernaturalEventsDreamVisit = "fiend";}

		if (supernaturalEventsResult >= 86 && supernaturalEventsResult <= 90) {SupernaturalEventsText = "A " + supernaturalEventsDreamVisit + " visited you in your dreams to give a warning of dangers to come.";}

		int supernaturalEventsPlanarVisitResult = roll(1,2);
		if (supernaturalEventsPlanarVisitResult == 1) {supernaturalEventsPlanarVisit = "Feywild";}
		if (supernaturalEventsPlanarVisitResult == 2) {supernaturalEventsPlanarVisit = "Shadowfell";}

		if (supernaturalEventsResult >= 91 && supernaturalEventsResult <= 95) {SupernaturalEventsText = "You briefly visited the " + supernaturalEventsPlanarVisit + ".";}
		if (supernaturalEventsResult >= 96 && supernaturalEventsResult <= 100) {SupernaturalEventsText = "You saw a portal that you believe leads to another plane of existence.";}
		return SupernaturalEventsText;
	}

	string RollTragedies ()
	{
		int tragediesResult = roll (1, 12);
		if (tragediesResult >= 1 && tragediesResult <= 2) {TragediesText = "A family member or a close friend died" + CauseOfDeath() + ".";}

		int tragedyEndOfFriendshipResult = roll(1,2);
		if (tragedyEndOfFriendshipResult == 1) {tragedyEndOfFriendship = "a misunderstanding";}
		if (tragedyEndOfFriendshipResult == 2) {tragedyEndOfFriendship = "something your former friend did";}

		if (tragediesResult == 3) {TragediesText = "A friendship ended bitterly, and the other person is now hostile to you. The cause was " + tragedyEndOfFriendship + ".";}
		if (tragediesResult == 4) {TragediesText = "You lost all your possessions in a disaster, and you had to rebuild your life.";}

		int tragedyImprisonmentTypeResult = roll(1,3);
			if (tragedyImprisonmentTypeResult == 1) {tragedyImprisonmentType = "in jail";}
			if (tragedyImprisonmentTypeResult == 2) {tragedyImprisonmentType = "shackled to an oar in a slave galley";}
			if (tragedyImprisonmentTypeResult == 3) {tragedyImprisonmentType = "performing hard labor";}

		if (tragediesResult == 5) {TragediesText = "You were imprisoned for a crime you didn’t commit and spent " + roll(1,6) + " years " + tragedyImprisonmentType + ".";}

		int tragedyCommunityDestructionAftermathResult = roll(1,2);
			if (tragedyCommunityDestructionAftermathResult == 1) {tragedyCommunityDestructionAftermath = "helped your town rebuild";}
			if (tragedyCommunityDestructionAftermathResult == 2) {tragedyCommunityDestructionAftermath = "moved somewhere else";}

		if (tragediesResult == 6) {TragediesText = "War ravaged your home community, reducing everything to rubble and ruin. In the aftermath, you " + tragedyCommunityDestructionAftermath + ".";}
		if (tragediesResult == 7) {TragediesText = "A lover disappeared without a trace. You have been looking for that person ever since.";}
		if (tragediesResult == 8) {TragediesText = "A terrible blight in your home community caused crops to fail, and many starved. You lost a sibling or some other family member.";}
		if (tragediesResult == 9) {TragediesText = "You did something that brought terrible shame to you in the eyes of your family. You might have been involved in a scandal, dabbled in dark magic, or offended someone important. The attitude of your family members toward you becomes indifferent at best, though they might eventually forgive you.";}

		int tragedyExileResult = roll(1,2);
			if (tragedyExileResult == 1) {tragedyExile = "wandered in the wilderness for a time";}
			if (tragedyExileResult == 2) {tragedyExile = "promptly found a new place to live";}

		if (tragediesResult == 10) {TragediesText = "For a reason you were never told, you were exiled from your community. You " + tragedyExile + ".";}

		int tragedyEndOfRomanceResult = roll(1,2);
			if (tragedyEndOfRomanceResult == 1) {tragedyEndOfRomance = "with bad feelings";}
			if (tragedyEndOfRomanceResult == 2) {tragedyEndOfRomance = "amicably";}

		if (tragediesResult == 11) {TragediesText = "A romantic relationship ended " + tragedyEndOfRomance + ".";}
		if (tragediesResult == 12) 
			{
			string tragedyPartnerDeath = CauseOfDeath();
			int checkForMurder = roll(1,12);
			if (tragedyPartnerDeath == " (murdered)") {if (checkForMurder == 1) {tragedyPartnerDeath += ". You were responsible, whether directly or indirectly";}}  
			TragediesText = "A current or prospective romantic partner of yours died" + tragedyPartnerDeath + ".";
			}
		return TragediesText;
	}

	string RollWar ()
	{
		int warResult = roll (1, 12);
			if (warResult == 1) {WarText = "You were knocked out and left for dead. You woke up hours later with no recollection of the battle.";}
			if (warResult >= 2 && warResult <= 3) {WarText = "You were badly injured in the fight, and you still bear the awful scars of those wounds.";}
			if (warResult == 4) {WarText = "You ran away from the battle to save your life, but you still feel shame for your cowardice.";}
			if (warResult >= 5 && warResult <= 7) {WarText = "You suffered only minor injuries, and the wounds all healed without leaving scars.";}
			if (warResult >= 8 && warResult <= 9) {WarText = "You survived the battle, but you suffer from terrible nightmares in which you relive the experience.";}
			if (warResult >= 10 && warResult <= 11) {WarText = "You escaped the battle unscathed, though many of your friends were injured or lost.";}
			if (warResult == 12) {WarText = "You acquitted yourself well in battle and are remembered as a hero. You might have received a medal for your bravery.";}
		return WarText;
	}

	string RollWeirdStuff ()
	{
		int weirdStuffResult = roll (1, 12);
		if (weirdStuffResult == 1) {WeirdStuffText = "You were turned into a toad and remained in that form for " + roll(1,4) + " weeks.";}
		if (weirdStuffResult == 2) {WeirdStuffText = "You were petrified and remained a stone statue for a time until someone freed you.";}
		if (weirdStuffResult == 3) {WeirdStuffText = "You were enslaved by a hag, a satyr, or some other being and lived in that creature’s thrall for " + roll(1,6) + " years.";}
		if (weirdStuffResult == 4) {WeirdStuffText = "A dragon held you as a prisoner for " + roll(1,4) + " months until adventurers killed it.";}

		int weirdStuffCaptorsResult = roll(1,3);
			if (weirdStuffCaptorsResult == 1) {weirdStuffCaptors = "drow";}
			if (weirdStuffCaptorsResult == 2) {weirdStuffCaptors = "kuo-toa";}
			if (weirdStuffCaptorsResult == 3) {weirdStuffCaptors = "quaggoths";}

		if (weirdStuffResult == 5) {WeirdStuffText = "You were taken captive by " + weirdStuffCaptors + ". You lived as a slave in the Underdark until you escaped.";}
		if (weirdStuffResult == 6) {WeirdStuffText = "You served a powerful adventurer as a hireling. You have only recently left that service. Your employer was a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollNPCClass() + ".";}
		if (weirdStuffResult == 7) {WeirdStuffText = "You went insane for " + roll(1,6) + " years and recently regained your sanity. A tic or some other bit of odd behavior might linger.";}
		if (weirdStuffResult == 8) {WeirdStuffText = "A lover of yours was secretly a silver dragon.";}
		if (weirdStuffResult == 9) {WeirdStuffText = "You were captured by a cult and nearly sacrificed on an altar to the foul being the cultists served. You escaped, but you fear they will find you.";}

		int weirdStuffMetAGodResult = roll(1,5);
			if (weirdStuffMetAGodResult == 1) {weirdStuffMetAGod = "a demigod";}
			if (weirdStuffMetAGodResult == 2) {weirdStuffMetAGod = "an archdevil";}
			if (weirdStuffMetAGodResult == 3) {weirdStuffMetAGod = "an archfey";}
			if (weirdStuffMetAGodResult == 3) {weirdStuffMetAGod = "a demon lord";}
			if (weirdStuffMetAGodResult == 3) {weirdStuffMetAGod = "a titan";}

		if (weirdStuffResult == 10) {WeirdStuffText = "You met " + weirdStuffMetAGod + " , and you lived to tell the tale.";}
		if (weirdStuffResult == 11) {WeirdStuffText = "You were swallowed by a giant fish and spent a month in its gullet before you escaped.";}
		if (weirdStuffResult == 12) {WeirdStuffText = "A powerful being granted you a wish, but you squandered it on something frivolous.";}
		return WeirdStuffText;
	}


}