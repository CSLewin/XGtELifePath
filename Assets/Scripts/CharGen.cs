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
	public string playerBackgroundSpecial = null;

	public string classQuirk1; 
	public string classQuirk2; 
	public string classQuirk3;

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

			GenerateCharacter ();

			displayText.text = "Press 'A' to generate a new character.\n\n";

			displayText.text += "You are a " + playerGender + " " + playerRace + " " + playerClass + playerSubclass + "! You are " + playerAge + " years old (or proportional non-human equivalent).\n" + 
				"Prior to adventuring, your were a(n): " + playerBackground + ".\n";

			displayText.text += "Personality " + roll (1, 8) + ", Ideal " + roll (1, 6) + ", Bond " + roll (1, 6) + ", Flaw " + roll (1, 6) + ". " + playerBackgroundSpecial + "\n\n";

			displayText.text += playerParentPresence + " " + mixedAncestry + playerMotherDetails + " " + playerFatherDetails + "\n\n";

			displayText.text += "You were born " + playerBirthplace + " You were raised " + RollFamily() + " Your lifestyle was " + playerLifestyle + 
				", and you lived " + playerHome + " " + playerChildhoodMemories + "\n\n";

			displayText.text += RollSiblings () + "\n";

			displayText.text += "You were a(n) " + playerBackground + " because " + playerBackgroundPersonalDecision + "\n\n";

			displayText.text += "You took up the life of a(n) " + playerClass + " because " + playerClassPersonalDecision + " " + RollClassQuirks() + "\n\n";

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
			if (occupationResult >= 1 && occupationResult <= 5) {occupation = "Academic";}
			if (occupationResult >= 6 && occupationResult <= 10) {occupation = "Adventurer";}
				if (occupation == "Adventurer") {occupation += " (" + RollNPCClass () + ")";}
			if (occupationResult == 11) {occupation = "Aristocrat";}
			if (occupationResult >= 12 && occupationResult <= 26) {occupation = "Artisan or Guild Member";}
			if (occupationResult >= 27 && occupationResult <= 31) {occupation = "Criminal";}
			if (occupationResult >= 32 && occupationResult <= 36) {occupation = "Entertainer";}
			if (occupationResult >= 37 && occupationResult <= 38) {occupation = "Exile, hermit, or refugee";}
			if (occupationResult >= 39 && occupationResult <= 43) {occupation = "Explorer or wanderer";}
			if (occupationResult >= 44 && occupationResult <= 55) {occupation = "Farmer or herder";}
			if (occupationResult >= 56 && occupationResult <= 60) {occupation = "Hunter or trapper";}
			if (occupationResult >= 61 && occupationResult <= 75) {occupation = "Laborer";}
			if (occupationResult >= 76 && occupationResult <= 80) {occupation = "Merchant";}
			if (occupationResult >= 81 && occupationResult <= 85) {occupation = "Politican or bureaucrat";}
			if (occupationResult >= 86 && occupationResult <= 90) {occupation = "Priest";}
			if (occupationResult >= 91 && occupationResult <= 95) {occupation = "Sailor";}
			if (occupationResult >= 96 && occupationResult <= 100) {occupation = "Soldier";}
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
			if (rolledRelationshipResult >= 3 && rolledRelationshipResult <= 4) {rolledRelationship = "Hostile";}
			if (rolledRelationshipResult >= 5 && rolledRelationshipResult <= 10) {rolledRelationship = "Friendly";}
			if (rolledRelationshipResult >= 11 && rolledRelationshipResult <= 12) {rolledRelationship = "Indifferent";}
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
			if (rolledStatusResult == 3) {rolledStatus = "Dead";}
				if (rolledStatus == "Dead") {rolledStatus += CauseOfDeath ();}
			if (rolledStatusResult >= 4 && rolledStatusResult <= 5) {rolledStatus = "Missing or unknown";}
			int badStatusResult = roll(1,3);
						if (badStatusResult == 1) {badStatus = "injury";}
						if (badStatusResult == 2) {badStatus = "financial trouble";}
						if (badStatusResult == 3) {badStatus = "relationship difficulties";}
			if (rolledStatusResult >= 6 && rolledStatusResult <= 8) {rolledStatus = "Alive, but doing poorly due to " + badStatus;}
			if (rolledStatusResult >= 9 && rolledStatusResult <= 12) {rolledStatus = "Alive and well";}
			if (rolledStatusResult >= 13 && rolledStatusResult <= 15) {rolledStatus = "Alive and quite successful";}
			if (rolledStatusResult >= 16 && rolledStatusResult <= 17) {rolledStatus = "Alive and infamous";}
			if (rolledStatusResult == 18) {rolledStatus = "Alive and famous";}
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
		{int subrace = roll (1, 2);
			if (subrace == 1) {playerRace = "Hill Dwarf";}
			if (subrace == 2) {playerRace = "Mountain Dwarf";}
		}

		if (raceResult >= 48 && raceResult <= 57) {playerRace = "Elf";}
		if (playerRace == "Elf") 
		{int subrace = roll (1, 3);
			if (subrace == 1) {playerRace = "High Elf";}
			if (subrace == 2) {playerRace = "Wood Elf";}
			if (subrace == 3) {playerRace = "Dark Elf"; childhoodMemoriesModifier += 1;}
		}

		if (raceResult >= 58 && raceResult <= 67) {playerRace = "Halfling";}
		if (playerRace == "Halfling") 
		{int subrace = roll (1, 2);
			if (subrace == 1) {playerRace = "Lightfoot Halfling"; childhoodMemoriesModifier += 1;}
			if (subrace == 2) {playerRace = "Stout Halfling";}
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

		if (raceResult >= 83 && raceResult <= 87) {playerRace = "Half-orc";}

		if (raceResult >= 88 && raceResult <= 92) {playerRace = "Tiefling"; childhoodMemoriesModifier += 1;}

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
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (Path of the Berserker)";}
			if (subclass == 2) {playerSubclass = " (Path of the Totem Warrior)";}
			if (subclass == 3) {playerSubclass = " (Path of the Ancestral Guardian)";}
			if (subclass == 4) {playerSubclass = " (Path of the Storm Herald)";}
			if (subclass == 5) {playerSubclass = " (Path of the Zealot)";}
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
		{int subclass = roll (1, 9);
			if (subclass == 1) {playerSubclass = " (Knowledge Domain)";}
			if (subclass == 2) {playerSubclass = " (Life Domain)";}
			if (subclass == 3) {playerSubclass = " (Light Domain)";}
			if (subclass == 4) {playerSubclass = " (Nature Domain)";}
			if (subclass == 5) {playerSubclass = " (Tempest Domain)";}
			if (subclass == 6) {playerSubclass = " (Trickery Domain)";}
			if (subclass == 7) {playerSubclass = " (War Domain)";}
			if (subclass == 8) {playerSubclass = " (Forge Domain)";}
			if (subclass == 9) {playerSubclass = " (Grave Domain)";}
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
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerSubclass = " (Champion)";}
			if (subclass == 2) {playerSubclass = " (Battle Master)";}
			if (subclass == 3) {playerSubclass = " (Eldritch Knight)";}
			if (subclass == 4) {playerSubclass = " (Arcane Archer)";}
			if (subclass == 5) {playerSubclass = " (Cavalier)";}
			if (subclass == 6) {playerSubclass = " (Samurai)";}
		}

		if (classResult == 6) {playerClass = "Monk";}
		if (playerClass == "Monk") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerSubclass = " (Way of the Open Hand)";}
			if (subclass == 2) {playerSubclass = " (Way of Shadow)";}
			if (subclass == 3) {playerSubclass = " (Way of the Four Elements)";}
			if (subclass == 4) {playerSubclass = " (Way of the Drunken Master)";}
			if (subclass == 5) {playerSubclass = " (Way of the Kensei)";}
			if (subclass == 6) {playerSubclass = " (Way of the Sun Soul)";}
		}

		if (classResult == 7) {playerClass = "Paladin"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Paladin") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (Oath of Devotion)";}
			if (subclass == 2) {playerSubclass = " (Oath of the Ancients)";}
			if (subclass == 3) {playerSubclass = " (Oath of Vengeance)";}
			if (subclass == 4) {playerSubclass = " (Oath of Conquest)";}
			if (subclass == 5) {playerSubclass = " (Oath of Redemption)";}
		}

		if (classResult == 8) {playerClass = "Ranger";}
		if (playerClass == "Ranger") 
		{int subclass = roll (1, 4);
			if (subclass == 1) {playerSubclass = " (Hunter)";}
			if (subclass == 2) {playerSubclass = " (Gloom Stalker)";}
			if (subclass == 3) {playerSubclass = " (Horizon Walker)";}
			if (subclass == 4) {playerSubclass = " (Monster Slayer)";}
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
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerSubclass = " (Archfey Patron)";}
			if (subclass == 2) {playerSubclass = " (Fiend Patron)";}
			if (subclass == 3) {playerSubclass = " (Great Old One Patron)";}
			if (subclass == 4) {playerSubclass = " (Celestial Patron)";}
			if (subclass == 5) {playerSubclass = " (Hexblade Patron)";}
		}

		if (classResult == 12) {playerClass = "Wizard";}
		if (playerClass == "Wizard") 
		{int subclass = roll (1, 9);
			if (subclass == 1) {playerSubclass = " (School of Abjuration)";}
			if (subclass == 2) {playerSubclass = " (School of Conjuration)";}
			if (subclass == 3) {playerSubclass = " (School of Divination)";}
			if (subclass == 4) {playerSubclass = " (School of Enchantment)";}
			if (subclass == 5) {playerSubclass = " (School of Evocation)";}
			if (subclass == 6) {playerSubclass = " (School of Illusion)";}
			if (subclass == 7) {playerSubclass = " (School of Necromancy)";}
			if (subclass == 8) {playerSubclass = " (School of Transmutation)";}
			if (subclass == 9) {playerSubclass = " (War Magic)";}
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
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Paladin") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Ranger") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Rogue") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Sorcerer") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Warlock") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		if (playerClass == "Wizard") {
			int classQuirk1Result = roll (1, 6);
			classQuirk1 = "";
			if (classQuirk1Result == 1) {classQuirk1 += "";}
			if (classQuirk1Result == 2) {classQuirk1 += "";}
			if (classQuirk1Result == 3) {classQuirk1 += "";}
			if (classQuirk1Result == 4) {classQuirk1 += "";}
			if (classQuirk1Result == 5) {classQuirk1 += "";}
			if (classQuirk1Result == 6) {classQuirk1 += "";}

			int classQuirk2Result = roll (1, 6);
			classQuirk2 = "";
			if (classQuirk2Result == 1) {classQuirk2 += "";}
			if (classQuirk2Result == 2) {classQuirk2 += "";}
			if (classQuirk2Result == 3) {classQuirk2 += "";}
			if (classQuirk2Result == 4) {classQuirk2 += "";}
			if (classQuirk2Result == 5) {classQuirk2 += "";}
			if (classQuirk2Result == 6) {classQuirk2 += "";}

			int classQuirk3Result = roll (1, 6);
			classQuirk3 = "";
			if (classQuirk3Result == 1) {classQuirk3 += "";}
			if (classQuirk3Result == 2) {classQuirk3 += "";}
			if (classQuirk3Result == 3) {classQuirk3 += "";}
			if (classQuirk3Result == 4) {classQuirk3 += "";}
			if (classQuirk3Result == 5) {classQuirk3 += "";}
			if (classQuirk3Result == 6) {classQuirk3 += "";}
		}

		string output = classQuirk1 + " " + classQuirk2 + " " + classQuirk3;
		return output;
	}

	string RollBackground () {
		int backgroundResult = roll (1, 13);
		if (backgroundResult == 1) {playerBackground = "Acolyte";}
		if (playerBackground == "Acolyte") {playerBackgroundSpecial = null;}

		if (backgroundResult == 2) {playerBackground = "Charlatan";}
		if (playerBackground == "Charlatan") {playerBackgroundSpecial = "Favorite Scheme: " + roll(1,6);}

		if (backgroundResult == 3 && flip == 1) {playerBackground = "Spy";}
		if (backgroundResult == 3 && flip == 2) {playerBackground = "Criminal";}
		if (playerBackground == "Criminal" || playerBackground == "Spy") {playerBackgroundSpecial = "Criminal Speciality: " + roll(1,8);}

		if (backgroundResult == 4 && flip == 1) {playerBackground = "Entertainer";}
		if (backgroundResult == 4 && flip == 2) {playerBackground = "Gladiator";}
		if (playerBackground == "Entertainer") {playerBackgroundSpecial = "Entertainer Routine: " + roll(1,8);}

		if (backgroundResult == 5) {playerBackground = "Folk Hero";}
		if (playerBackground == "Folk Hero") {playerBackgroundSpecial = "Defining Event: " + roll(1,10);}

		if (backgroundResult == 6 && flip == 1) {playerBackground = "Guild Artisan";}
		if (backgroundResult == 6 && flip == 2) {playerBackground = "Guild Merchant";}
		if (playerBackground == "Guild Artisan" || playerBackground == "Guild Merchant") {playerBackgroundSpecial = "Guild Business: " + roll(1,20);}

		if (backgroundResult == 7) {playerBackground = "Hermit";}
		if (playerBackground == "Hermit") {playerBackgroundSpecial = "Life of Seclusion: " + roll(1,8);}

		if (backgroundResult == 8 && flip == 1) {playerBackground = "Noble";}
		if (backgroundResult == 8 && flip == 2) {playerBackground = "Knight";}
		if (playerBackground == "Noble" || playerBackground == "Knight") {playerBackgroundSpecial = null;}

		if (backgroundResult == 9) {playerBackground = "Outlander";}
		if (playerBackground == "Outlander") {playerBackgroundSpecial = "Origin: " + roll(1,10);}

		if (backgroundResult == 10) {playerBackground = "Sage";}
		if (playerBackground == "Sage") {playerBackgroundSpecial = "Specialty: " + roll(1,8);}

		if (backgroundResult == 11 && flip == 1) {playerBackground = "Sailor";}
		if (backgroundResult == 11 && flip == 2) {playerBackground = "Pirate";}
		if (playerBackground == "Sailor" || playerBackground == "Pirate") {playerBackgroundSpecial = null;}

		if (backgroundResult == 12) {playerBackground = "Soldier";}
		if (playerBackground == "Soldier") {playerBackgroundSpecial = "Specialty: " + roll(1,8);}

		if (backgroundResult == 13) {playerBackground = "Urchin";}
		if (playerBackground == "Urchin") {playerBackgroundSpecial = null;}

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
		if (playerRace == "Hill Dwarf" || playerRace == "Mountain Dwarf" || playerRace == "High Elf" || playerRace == "Wood Elf" || playerRace == "Dark Elf") {numberOfSiblingsResult -= 2;}
		if (numberOfSiblingsResult <= 2) {playerNumberOfSiblings = 0;}
		if (numberOfSiblingsResult >= 3 && numberOfSiblingsResult <= 4) {playerNumberOfSiblings = roll(1,3);}
		if (numberOfSiblingsResult >= 5 && numberOfSiblingsResult <= 6) {playerNumberOfSiblings = roll(1,4) + 1;}
		if (numberOfSiblingsResult >= 7 && numberOfSiblingsResult <= 8) {playerNumberOfSiblings = roll(1,6) + 2;}
		if (numberOfSiblingsResult >= 9 && numberOfSiblingsResult <= 10) {playerNumberOfSiblings = roll(1,8) + 3;}

		//Determine birth order, gender, alignment, occupation, relationship, status for each sibling
		int playerNumberOfSiblingsCounter = playerNumberOfSiblings;
		for (int i = 1; i <= playerNumberOfSiblingsCounter; i++) 
		{
			siblingText += "Sibling #" + i + ": " + RollBirthOrder() + ", " + RollGender () + ", " + RollAlignment () + ", " + RollOccupation () + ". " +
				RollRelationship () + " relationship. " + RollNPCStatus () + ".\n";
		}
		return "You have " + playerNumberOfSiblings + " sibling(s).\n" + siblingText;
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
		if (familyResult >= 76 && familyResult <= 100) {playerFamily = "by your mother and father.";}
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
			if (playerRace == "Half-elf") {
				int mixedAncestryResultsHalfElf = roll (1, 8);
				if (mixedAncestryResultsHalfElf <= 5) {
					mixedAncestry = "One parent was an elf and the other was a human.";
				}
				if (mixedAncestryResultsHalfElf == 6) {
					mixedAncestry = "One parent was an elf and the other was a half-elf.";
				}
				if (mixedAncestryResultsHalfElf == 7) {
					mixedAncestry = "One parent was a human and the other was a half-elf.";
				}
				if (mixedAncestryResultsHalfElf == 8) {
					mixedAncestry = "Both parents were half-elves.";
				}
			}

			if (playerRace == "Half-orc") {
				int mixedAncestryResultsHalfOrc = roll (1, 8);
				if (mixedAncestryResultsHalfOrc <= 3) {
					mixedAncestry = "One parent was an orc and the other was a human.";
				}
				if (mixedAncestryResultsHalfOrc >= 4 && mixedAncestryResultsHalfOrc <= 5) {
					mixedAncestry = "One parent was an orc and the other was a half-orc.";
				}
				if (mixedAncestryResultsHalfOrc >= 6 && mixedAncestryResultsHalfOrc <= 7) {
					mixedAncestry = "One parent was a human and the other was a half-orc.";
				}
				if (mixedAncestryResultsHalfOrc == 8) {
					mixedAncestry = "Both parents were half-orcs.";
				}
			}

			if (playerRace == "Tiefling") {
				int mixedAncestryResultsTiefling = roll (1, 8);
				if (mixedAncestryResultsTiefling <= 4) {
					mixedAncestry = "Both parents were humans, their infernal heritage dormant until you came along.";
				}
				if (mixedAncestryResultsTiefling >= 5 && mixedAncestryResultsTiefling <= 6) {
					mixedAncestry = "One parent was a tiefling and the other was a human.";
				}
				if (mixedAncestryResultsTiefling == 7) {
					mixedAncestry = "One parent was a tiefling and the other was a devil.";
				}
				if (mixedAncestryResultsTiefling == 8) {
					mixedAncestry = "One parent was a human and the other was a devil.";
				}
			}

			//Roll mother's alignment, occupation, relationship, status
			playerMotherDetails = "Your mother's was " + RollAlignment () + " and spent her days as a(n) " + RollOccupation () + ". " +
				"You were " + RollRelationship () + " toward each other. Your mother is " + RollNPCStatus() + ".";


			//Roll father's alignment, occupation, relationship, status
			playerFatherDetails = "Your father was " + RollAlignment() + " and spent his days as a(n) " + RollOccupation() + ". " +
				"You were " + RollRelationship () + " toward each other. Your father is " + RollNPCStatus() + ".";
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
		if (playerBackground == "Acolyte") 
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you ran away form home at an early age and found refuge in a temple.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "your family gave you to a temple, since they were unable or unwilling to care for me.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you grew up in a household with strong religious convictions. Entering the service of one or more gods seemed natural.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "an impassioned sermon struck a chord deep in your soul and moved you to serve the faith.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you followed a childhood friend, a respected acquaintance, or someone you loved into religious service.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "after encountering a true servant of the gods, you were so inspired that you immediately entered the service of a religious group.";}
		}

		if (playerBackground == "Charlatan") 
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you were left to your own devices, and your knack for manipulating others helped you survive.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you learned early on that people were gullible and easy to exploit.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you often got in trouble, but you managed to talk your way out of it every time.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you took up with a confidence artist, from whom you learned your craft.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "after a charlatan fleeced your family, you decided to learn the trade so you would never be fooled by such deception again";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you were poor or feared becoming poor, so you learned the tricks needed to keep yourself out of poverty.";}
		}

		if (playerBackground == "Spy" || playerBackground == "Criminal")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you resented authority in your younger days and saw a life of crime as the best way to fight against tyranny and oppression.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "necessity forced you to take up the life, since it was the only way you could survive.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you fell in with a gang of reprobates and ne'er-do-wells, and you learned your speciality from them.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "a parent or relative taught you your criminal speciality to prepare you for the family business.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you left home and found ap lace in a thieves' guild or some other criminal organization.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you were always bored, so you turned to crime to pass the time and discovered you were quite good at it.";}
		}

		if (playerBackground == "Entertainer" || playerBackground == "Gladiator")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "members of your family made ends meet by performing, so it was fitting for you to follow their example.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you always had a keen insight into other people, enough so that you could make them laugh or cry with your stories or songs.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you ran away from home to follow a minstrel troupe.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you saw a bard perform once, and you knew from that moment on what you were born to do.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you earned coin by performing on street corners and eventually made a name for yourself.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "a traveling entertainer took you in and taught you the trade.";}
		}

		if (playerBackground == "Folk Hero")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you learned what was right and wrong frmo your family.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you were always enamored by tales of heroes and wished you could be something more than ordinary.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you hated your mundane life, so when it was time for someone to step up and do the right thing, you took your chance.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "a parent or one of your relatives was an adventurer, and you were inspired by that person’s courage.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "a mad old hermit spoke a prophecy when you was born, saying that you would accomplish great things.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you have always stood up for those who are weaker than you are.";}
		}

		if (playerBackground == "Guild Artisan" || playerBackground == "Guild Merchant")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you were apprenticed to a master who taught you the guild’s business.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you helped a guild artisan keep a secret or complete a task, and in return you were taken on as an apprentice.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "one of your family members who belonged to the guild made a place for you.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you were always good with your hands, so you took the opportunity to learn a trade.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you wanted to get away from your home situation and start a new life.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you learned the essentials of your craft from a mentor but had to join the guild to finish your training.";}
		}

		if (playerBackground == "Hermit")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "your enemies ruined your reputation, and you fled to the wilds to avoid further disparagement.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you are comfortable with being isolated, as you seek inner peace.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you never liked the people you called your friends, so it was easy for you to strike out on your own.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you felt compelled to forsake your past, but did so with great reluctance, and sometimes you regret making that decision.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you lost everything — your home, your family, your friends. Going it alone was all you could do.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "society’s decadence disgusted you, so you decided to leave it behind.";}
		}

		if (playerBackground == "Noble" || playerBackground == "Knight")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you come from an old and storied family, and it fell to you to preserve the family name.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "your family has been disgraced, and you intend to clear your name.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "your family recently came by its title, and that elevation thrust you into a new and strange world.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "your family has a title, but none of your ancestors have distinguished themselves since they gained it.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "your family is filled with remarkable people. You hope to live up to their example.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you hope to increase your family’s power and influence.";}
		}

		if (playerBackground == "Outlander")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you spent a lot of time in the wilderness as a youngster, and you came to love that way of life.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "from a young age,  couldn’t abide the stink of the cities and preferred to spend your time in nature.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you came to understand the darkness that lurks in the wilds, and you vowed to combat it.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "your people lived on the edges of civilization, and you learned the methods of survival from your family.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "after a tragedy you retreated to the wilderness, leaving your old life behind.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "your family moved away from civilization, and you learned to adapt to your new environment.";}
		}

		if (playerBackground == "Sage")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you were naturally curious, so you packed up and went to a university to learn more about the world.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "your mentor’s teachings opened your mind to new possibilities in that field of study.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you were always an avid reader, and you learned much about your favorite topic on your own.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you discovered an old library and pored over the texts you found there. That experience awakened a hunger for more knowledge.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you impressed a wizard who told you you were squandering your talents and should seek out an education to take advantage of your gifts.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "one of your parents or a relative gave you a basic education that whetted your appetite, and you left home to build on what you had learned.";}
		}

		if (playerBackground == "Sailor" || playerBackground == "Pirate")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you were press-ganged by pirates and forced to serve on their ship until you finally escaped.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you wanted to see the world, so you signed on as a deckhand for a merchant ship.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "one of your relatives was a sailor who took you to sea.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "you needed to escape your community quickly, so you stowed away on a ship. When the crew found you, you were forced to work for your passage.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "reavers attacked your community, so you found refuge on a ship until you could seek vengeance.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "you had few prospects where you were living, so you left to find your fortune elsewhere.";}
		}

		if (playerBackground == "Soldier")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you joined the militia to help protect your community from monsters.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "a relative of yours was a soldier, and you wanted to carry on the family tradition.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "the local lord forced you to enlist in the army.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "war ravaged your homeland while you were growing up. Fighting was the only life you ever knew.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you wanted fame and fortune, so you joined a mercenary company, selling your sword to the highest bidder.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "invaders attacked your homeland. It was your duty to take up arms in defense of your people.";}
		}

		if (playerBackground == "Urchin")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "wanderlust caused you to leave your family to see the world. You look after yourself.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you ran away from a bad situation at home and made your own way in the world.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "monsters wiped out your village, and you were the sole survivor. You had to find a way to survive.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "a notorious thief looked after you and other orphans, and you spied and stole to earn your keep.";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "one day you woke up on the streets, alone and hungry, with no memory of your early childhood.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "your parents died, leaving no one to look after you. You raised yourself.";}
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
			if (classPersonalDecisionResult == 5) {playerClassPersonalDecision = "your future patron visited you in your dreams and offered great power in exchange for your service.\t";}
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
		if (ageResult >1 && ageResult <=20) {playerAge = Random.Range(18,21); playerLifeEventCount = 1;}
		if (ageResult >21 && ageResult <=59) {playerAge = Random.Range(21,31); playerLifeEventCount = roll(1,4);}
		if (ageResult >60 && ageResult <=69) {playerAge = Random.Range(31,41); playerLifeEventCount = roll(1,6);}
		if (ageResult >70 && ageResult <=89) {playerAge = Random.Range(41,51); playerLifeEventCount = roll(1,8);}
		if (ageResult >90 && ageResult <=99) {playerAge = Random.Range(51,61); playerLifeEventCount = roll(1,10);}
		if (ageResult == 100) {playerAge = Random.Range(61,106); playerLifeEventCount = roll(1,12);}
		return;
	}

	string RollLifeEvents ()
	{
		lifeEventResult = roll(1,100);
		if (lifeEventResult >= 1 && lifeEventResult <= 10) {lifeEventText = "You suffered a tragedy: " + RollTragedies () + "\n";}
		if (lifeEventResult >= 11 && lifeEventResult <= 20) {lifeEventText = "You gained a bit of good fortune: " + RollBoons () + "\n";}
		if (lifeEventResult >= 21 && lifeEventResult <= 30) {lifeEventText = "You fell in love or got married. If you get this result more than once, you can choose to have a child instead. Work with your DM to determine the identity of your love interest.\n";}

		int lifeEventEnemyBlameResult = roll(1,2);
		if (lifeEventEnemyBlameResult == 1) {lifeEventEnemyBlame = "and you are to blame";}
		if (lifeEventEnemyBlameResult == 2) {lifeEventEnemyBlame = "but you are blameless";}

		if (lifeEventResult >= 31 && lifeEventResult <= 40) {lifeEventText = "You made an enemy of an adventurer " + lifeEventEnemyBlame + ". Your enemy is a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollNPCClass() + ".\n";}
		if (lifeEventResult >= 41 && lifeEventResult <= 50) {lifeEventText = "You made a friend of an adventurer. Your friend is a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollNPCClass() + ". Work with your DM to establish how your friendship began.\n";}
		if (lifeEventResult >= 51 && lifeEventResult <= 70) {lifeEventText = "You spent time working in a job related to your background. Start the game with an extra " + roll(2,6) + " gp.\n";}
		if (lifeEventResult >= 71 && lifeEventResult <= 75) {lifeEventText = "You met someone important. They are a " + RollAlignment() + " " + RollGender() + " " + RollRace() + " " + RollOccupation() + " and feel " + RollRelationship() + " toward you. Work out additional details with your DM as needed to fit this character into your backstory.\n";}
		if (lifeEventResult >= 76 && lifeEventResult <= 80) {lifeEventText = "You went on an adventure: " + RollAdventure() + "\n";}
		if (lifeEventResult >= 81 && lifeEventResult <= 85) {lifeEventText = "You had a supernatural experience: " + RollSupernaturalEvents() + "\n";}
		if (lifeEventResult >= 86 && lifeEventResult <= 90) {lifeEventText = "You fought in a battle. " + RollWar() + "\n";}
		if (lifeEventResult >= 91 && lifeEventResult <= 95) {lifeEventText = "You committed " + RollCrime() + " or were wrongly accused of doing so. " + RollPunishment() + "\n";}
		if (lifeEventResult >= 96 && lifeEventResult <= 99) {lifeEventText = "You encountered something magical: " + RollArcaneMatters() + "\n";}
		if (lifeEventResult == 100) {lifeEventText = "Something truly strange happened to you: " + RollWeirdStuff() + "\n";}
		return lifeEventText;
	}

	string PrintLifeEvents() 
	{
		int playerLifeEventCounter = playerLifeEventCount;
		for (int i = 1; i <= playerLifeEventCounter; i++) 
		{
			lifeEventText += "Event #" + i + ": " + RollLifeEvents();
		}
		return "You experienced " + playerLifeEventCount + " major events in your life so far: \n" + lifeEventText;
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
		if (AdventureResult == 100) {lifeEventText = "You came across a common magic item (of the DM’s choice).\n";}
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
		if (boonsResult == 2) {BoonsText = "You saved the life of a commoner, who now owes you a life debt. This individual accompanies you on your travels and performs mundane tasks for you, but will leave if neglected, abused, or imperiled. Your companion is a " + RollAlignment() + " " + RollGender() + " " + RollRace();}
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
		if (crimeResult == 1) {CrimeText = "Murder";}
		if (crimeResult == 2) {CrimeText = "Theft";}
		if (crimeResult == 3) {CrimeText = "Burglary";}
		if (crimeResult == 4) {CrimeText = "Assault";}
		if (crimeResult == 5) {CrimeText = "Smuggling";}
		if (crimeResult == 6) {CrimeText = "Kidnapping";}
		if (crimeResult == 7) {CrimeText = "Extortion";}
		if (crimeResult == 8) {CrimeText = "Counterfeiting";}
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
		if (punishmentServedTimeResult == 2) {punishmentServedTime = "escaped after " + roll(1,4) + "years";}

		if (punishmentResult >= 9 && punishmentResult <= 12) {PunishmentText = "You were caught and convicted. You spent time " + punishmentImprisonmentType + ". You " + punishmentServedTime;}
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
			if (tragedyEndOfRomanceResult == 1) {tragedyEndOfRomance = " with bad feelings";}
			if (tragedyEndOfRomanceResult == 2) {tragedyEndOfRomance = " amicably";}

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