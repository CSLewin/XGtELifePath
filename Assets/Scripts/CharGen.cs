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
	public int childhoodMemoriesModifier = 0;
	public string playerBackground = "testbackground";
	public string playerBackgroundSpecial = null;

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

	void Awake () {
		displayText.text = null;
	}

	// Use this for initialization
	void Start () {
		displayText.text += "Press 'A' to generate a new character.\n";

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

			GenerateCharacter ();

			displayText.text = "Press 'A' to generate a new character.\n\n";

			displayText.text += "You are a " + playerGender + " " + playerRace + " " + playerClass + "! Prior to adventuring, your were a(n): " + playerBackground + ".\n";
			displayText.text += "Personality " + roll (1, 8) + ", Ideal " + roll (1, 6) + ", Bond " + roll (1, 6) + ", Flaw " + roll (1, 6) + ". " + playerBackgroundSpecial + "\n\n";

			displayText.text += playerParentPresence + " " + mixedAncestry + playerMotherDetails + " " + playerFatherDetails + "\n\n";

			displayText.text += "You were born " + playerBirthplace + " You were raised " + RollFamily() + " Your lifestyle was " + playerLifestyle + 
				", and you lived " + playerHome + " " + playerChildhoodMemories + "\n\n";

			displayText.text += RollSiblings () + "\n";

			displayText.text += "You became a(n) " + playerBackground + " because " + playerBackgroundPersonalDecision;
				

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
		int rolledStatusResult = roll (3, 6);
			if (rolledStatusResult == 3) {rolledStatus = "dead";}
				if (rolledStatus == "Dead") {rolledStatus += CauseOfDeath ();}
			if (rolledStatusResult >= 4 && rolledStatusResult <= 5) {rolledStatus = "missing or unknown";}
			if (rolledStatusResult >= 6 && rolledStatusResult <= 8) {rolledStatus = "alive, but doing poorly due to injury, financial trouble, or relationship difficulties";}
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
			if (subclass == 1) {playerClass += " (Path of the Berserker)";}
			if (subclass == 2) {playerClass += " (Path of the Totem Warrior)";}
			if (subclass == 3) {playerClass += " (Path of the Ancestral Guardian)";}
			if (subclass == 4) {playerClass += " (Path of the Storm Herald)";}
			if (subclass == 5) {playerClass += " (Path of the Zealot)";}
		}

		if (classResult == 2) {playerClass = "Bard";}
		if (playerClass == "Bard")
			childhoodMemoriesModifier += 2;
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerClass += " (College of Lore)";}
			if (subclass == 2) {playerClass += " (College of Valor)";}
			if (subclass == 3) {playerClass += " (College of Glamour)";}
			if (subclass == 4) {playerClass += " (College of Swords)";}
			if (subclass == 5) {playerClass += " (College of Whispers)";}
		}

		if (classResult == 3) {playerClass = "Cleric";}
		if (playerClass == "Cleric") 
		{int subclass = roll (1, 9);
			if (subclass == 1) {playerClass += " (Knowledge Domain)";}
			if (subclass == 2) {playerClass += " (Life Domain)";}
			if (subclass == 3) {playerClass += " (Light Domain)";}
			if (subclass == 4) {playerClass += " (Nature Domain)";}
			if (subclass == 5) {playerClass += " (Tempest Domain)";}
			if (subclass == 6) {playerClass += " (Trickery Domain)";}
			if (subclass == 7) {playerClass += " (War Domain)";}
			if (subclass == 8) {playerClass += " (Forge Domain)";}
			if (subclass == 9) {playerClass += " (Grave Domain)";}
		}

		if (classResult == 4) {playerClass = "Druid";}
		if (playerClass == "Druid") 
		{int subclass = roll (1, 4);
			if (subclass == 1) {playerClass += " (Circle of the Land)";}
			if (subclass == 2) {playerClass += " (Circle of the Moon)";}
			if (subclass == 3) {playerClass += " (Circle of Dreams)";}
			if (subclass == 4) {playerClass += " (Circle of the Shepherd)";}
		}

		if (classResult == 5) {playerClass = "Fighter";}
		if (playerClass == "Fighter") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerClass += " (Champion)";}
			if (subclass == 2) {playerClass += " (Battle Master)";}
			if (subclass == 3) {playerClass += " (Eldritch Knight)";}
			if (subclass == 4) {playerClass += " (Arcane Archer)";}
			if (subclass == 5) {playerClass += " (Cavalier)";}
			if (subclass == 6) {playerClass += " (Samurai)";}
		}

		if (classResult == 6) {playerClass = "Monk";}
		if (playerClass == "Monk") 
		{int subclass = roll (1, 6);
			if (subclass == 1) {playerClass += " (Way of the Open Hand)";}
			if (subclass == 2) {playerClass += " (Way of Shadow)";}
			if (subclass == 3) {playerClass += " (Way of the Four Elements)";}
			if (subclass == 4) {playerClass += " (Way of the Drunken Master)";}
			if (subclass == 5) {playerClass += " (Way of the Kensei)";}
			if (subclass == 6) {playerClass += " (Way of the Sun Soul)";}
		}

		if (classResult == 7) {playerClass = "Paladin"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Paladin") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerClass += " (Oath of Devotion)";}
			if (subclass == 2) {playerClass += " (Oath of the Ancients)";}
			if (subclass == 3) {playerClass += " (Oath of Vengeance)";}
			if (subclass == 4) {playerClass += " (Oath of Conquest)";}
			if (subclass == 5) {playerClass += " (Oath of Redemption)";}
		}

		if (classResult == 8) {playerClass = "Ranger";}
		if (playerClass == "Ranger") 
		{int subclass = roll (1, 4);
			if (subclass == 1) {playerClass += " (Hunter)";}
			if (subclass == 2) {playerClass += " (Gloom Stalker)";}
			if (subclass == 3) {playerClass += " (Horizon Walker)";}
			if (subclass == 4) {playerClass += " (Monster Slayer)";}
		}

		if (classResult == 9) {playerClass = "Rogue";}
		if (playerClass == "Rogue") 
		{int subclass = roll (1, 7);
			if (subclass == 1) {playerClass += " (Thief)";}
			if (subclass == 2) {playerClass += " (Assassin)";}
			if (subclass == 3) {playerClass += " (Arcane Trickster)";}
			if (subclass == 4) {playerClass += " (Inquisitive)";}
			if (subclass == 5) {playerClass += " (Mastermind)";}
			if (subclass == 6) {playerClass += " (Scout)";}
			if (subclass == 7) {playerClass += " (Swashbuckler)";}
		}

		if (classResult == 10) {playerClass = "Sorcerer"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Sorcerer") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerClass += " (Draconic Bloodline)";}
			if (subclass == 2) {playerClass += " (Wild Magic)";}
			if (subclass == 3) {playerClass += " (Divine Soul)";}
			if (subclass == 4) {playerClass += " (Shadow Magic)";}
			if (subclass == 5) {playerClass += " (Storm Sorcery)";}
		}

		if (classResult == 11) {playerClass = "Warlock"; childhoodMemoriesModifier += 2;}
		if (playerClass == "Warlock") 
		{int subclass = roll (1, 5);
			if (subclass == 1) {playerClass += " (Archfey Patron)";}
			if (subclass == 2) {playerClass += " (Fiend Patron)";}
			if (subclass == 3) {playerClass += " (Great Old One Patron)";}
			if (subclass == 4) {playerClass += " (Celestial Patron)";}
			if (subclass == 5) {playerClass += " (Hexblade Patron)";}
		}

		if (classResult == 12) {playerClass = "Wizard";}
		if (playerClass == "Wizard") 
		{int subclass = roll (1, 9);
			if (subclass == 1) {playerClass += " (School of Abjuration)";}
			if (subclass == 2) {playerClass += " (School of Conjuration)";}
			if (subclass == 3) {playerClass += " (School of Divination)";}
			if (subclass == 4) {playerClass += " (School of Enchantment)";}
			if (subclass == 5) {playerClass += " (School of Evocation)";}
			if (subclass == 6) {playerClass += " (School of Illusion)";}
			if (subclass == 7) {playerClass += " (School of Necromancy)";}
			if (subclass == 8) {playerClass += " (School of Transmutation)";}
			if (subclass == 9) {playerClass += " (War Magic)";}
		}

		return playerClass;

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
			siblingText += "Sibling #" + i + ": " + RollBirthOrder() + " " + RollGender () + " " + RollAlignment () + " " + RollOccupation () + ". " +
				RollRelationship () + " relationship. " + RollNPCStatus () + ".\n";
		}
		return "You have " + playerNumberOfSiblings + " sibling(s).\n" + siblingText;
	}

	string RollFamily() {
		int familyResult = roll (1, 100);
		if (familyResult == 1) {playerFamily = "by nobody at all. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult == 2) {playerFamily = "in an institution, such as an asylum. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult == 3) {playerFamily = "in a temple. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 4 && familyResult <= 5) {playerFamily = "in an orphanage. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 6 && familyResult <= 7) {playerFamily = "by a guardian. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 8 && familyResult <= 15 && flip == 1) {playerFamily = "by your aunt and/or uncle. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 8 && familyResult <= 15 && flip == 2) {playerFamily = "by your extended family, tribe, or clan. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 16 && familyResult <= 25 && flip == 1) {playerFamily = "by your paternal grandparents. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 16 && familyResult <= 25 && flip == 2) {playerFamily = "by your maternal grandparents. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 26 && familyResult <= 35 && flip == 1) {playerFamily = "by an adoptive family of the same race. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 26 && familyResult <= 35 && flip == 2) {playerFamily = "by an adoptive family of a different race. Your mother " + AbsentParent() + " Your father " + AbsentParent();}
		if (familyResult >= 36 && familyResult <= 55 && flip == 1) {playerFamily = "by a single father. Your mother " + AbsentParent();}
		if (familyResult >= 36 && familyResult <= 55 && flip == 2) {playerFamily = "by a single stepfather. Your mother " + AbsentParent();}
		if (familyResult >= 56 && familyResult <= 75 && flip == 1) {playerFamily = "by a single mother. Your father " + AbsentParent();}
		if (familyResult >= 56 && familyResult <= 75 && flip == 2) {playerFamily = "by a single stepmother. Your father " + AbsentParent();}
		if (familyResult >= 76 && familyResult <= 100) {playerFamily = "by your mother and father.";}
		return playerFamily;
	}

	string AbsentParent(){
		int absentParentResult = roll (1, 4);
		if (absentParentResult == 1) {absentParent = "died" + CauseOfDeath() + ".";}
		if (absentParentResult == 2) {absentParent = "was imprisoned, enslaved, or otherwise taken away.";}
		if (absentParentResult == 3) {absentParent = "abandoned you.";}
		if (absentParentResult == 4) {absentParent = "disappeared to an unknown fate.";}
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
		//roll parents
		//roll racial-specific parent, if necessary

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
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "you earned coin by performing on street corners and eventually making a name for yourself.";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "a traveling entertainer took you in and taught you the trade.";}
		}

		if (playerBackground == "Folk Hero")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "you learned what was right and wrong frmo your family.";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "you were always enamored by tales of heroes and wished you could be something more than ordinary.";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "you hated your mundane life, so when it was time for someone to step up and do the right thing, you took your chance.";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Guild Artisan" || playerBackground == "Guild Merchant")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Hermit")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Noble" || playerBackground == "Knight")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Outlander")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Sage")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Sailor" || playerBackground == "Pirate")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Soldier")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}

		if (playerBackground == "Urchin")
		{
			if (backgroundPersonalDecisionResult == 1) {playerBackgroundPersonalDecision = "1";}
			if (backgroundPersonalDecisionResult == 2) {playerBackgroundPersonalDecision = "2";}
			if (backgroundPersonalDecisionResult == 3) {playerBackgroundPersonalDecision = "3";}
			if (backgroundPersonalDecisionResult == 4) {playerBackgroundPersonalDecision = "4";}
			if (backgroundPersonalDecisionResult == 5) {playerBackgroundPersonalDecision = "5";}
			if (backgroundPersonalDecisionResult == 6) {playerBackgroundPersonalDecision = "6";}
		}
		
		return "You became a(n) " + playerBackground + "because " + playerBackgroundPersonalDecision;
	}

	void GeneratePersonalDecisions () {
		//Roll Background details
		RollBackgroundPersonalDecision();

		//Roll Class Training
			//Roll specific class detail tables

	}

	void GenerateLifeEvents () {
		//Roll age
			//roll # of life events

		//Roll each life event
			//roll life event sub-tables

	}
}
