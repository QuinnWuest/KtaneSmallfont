﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Rnd = UnityEngine.Random;
using KModkit;

public class SmallfontScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public KMRuleSeedable RuleSeedable;

    public KMSelectable[] SquareSels;
    public GameObject[] SquareObjs;
    public GameObject StatusLightObj;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private static readonly string[] _fullWordList = {
            "ABACK", "ABBEY", "ABBOT", "ABIDE", "ABORT", "ABOUT", "ABOVE", "ABUSE", "ABYSS", "ACIDS", "ACORN", "ACRES", "ACTED", "ACTOR", "ACUTE", "ADAPT", "ADDED", "ADDER", "ADDLE", "ADIEU", "ADIOS", "ADMIN", "ADMIT", "ADOPT", "ADORE", "ADORN", "ADULT", "AFFIX", "AFTER", "AGAIN", "AGENT", "AGILE", "AGING", "AGONY", "AGORA", "AGREE", "AHEAD", "AIDED", "AIMED", "AIOLI", "AIRED", "AISLE", "ALARM", "ALBUM", "ALERT", "ALGAE", "ALIAS", "ALIBI", "ALIEN", "ALIGN", "ALIKE", "ALIVE", "ALLAY", "ALLEY", "ALLOT", "ALLOW", "ALLOY", "ALOFT", "ALONE", "ALONG", "ALOOF", "ALOUD", "ALPHA", "ALTAR", "ALTER", "AMASS", "AMAZE", "AMBER", "AMBLE", "AMEND", "AMISH", "AMISS", "AMONG", "AMPLE", "AMUSE", "ANGEL", "ANGER", "ANGLE", "ANGLO", "ANGRY", "ANGST", "ANIME", "ANION", "ANISE", "ANKLE", "ANNEX", "ANNOY", "ANNUL", "ANTIC", "ANVIL", "AORTA", "APART", "APNEA", "APPLE", "APPLY", "APRON", "AREAS", "ARENA", "ARGUE", "ARISE", "ARMED", "ARMOR", "AROMA", "AROSE", "ARRAY", "ARROW", "ARSON", "ASHEN", "ASHES", "ASIAN", "ASIDE", "ASKED", "ASSAY", "ASSET", "ASTER", "ASTIR", "ATOLL", "ATOMS", "ATONE", "ATTIC", "AUDIO", "AUDIT", "AUGUR", "AUNTY", "AVAIL", "AVIAN", "AVOID", "AWAIT", "AWAKE", "AWARD", "AWARE", "AWASH", "AWFUL", "AWOKE", "AXIAL", "AXIOM", "AXION", "AZTEC",
            "BACKS", "BACON", "BADGE", "BADLY", "BAKED", "BAKER", "BALLS", "BANDS", "BANKS", "BARGE", "BARON", "BASAL", "BASED", "BASES", "BASIC", "BASIL", "BASIN", "BASIS", "BATCH", "BATHS", "BATTY", "BEACH", "BEADS", "BEAMS", "BEANS", "BEARD", "BEARS", "BEAST", "BEECH", "BEERS", "BEGAN", "BEGIN", "BEGUN", "BEING", "BELLS", "BELLY", "BELOW", "BELTS", "BENCH", "BERRY", "BIBLE", "BIDET", "BIGHT", "BIKES", "BILGE", "BILLS", "BINGE", "BINGO", "BIOME", "BIRCH", "BIRDS", "BIRTH", "BISON", "BITCH", "BITER", "BLACK", "BLADE", "BLAME", "BLAND", "BLANK", "BLARE", "BLAST", "BLAZE", "BLEAK", "BLEAT", "BLEED", "BLEEP", "BLEND", "BLESS", "BLIMP", "BLIND", "BLING", "BLINK", "BLISS", "BLITZ", "BLOCK", "BLOKE", "BLOND", "BLOOD", "BLOOM", "BLOOP", "BLOWN", "BLOWS", "BLUES", "BLUFF", "BLUNT", "BLUSH", "BOARD", "BOATS", "BOGGY", "BOGUS", "BOLTS", "BOMBS", "BONDS", "BONED", "BONER", "BONES", "BONNY", "BONUS", "BOOKS", "BOOST", "BOOTH", "BOOTS", "BORAX", "BORED", "BORER", "BORNE", "BORON", "BOTCH", "BOUGH", "BOULE", "BOUND", "BOWED", "BOWEL", "BOWLS", "BOXED", "BOXER", "BOXES", "BRACE", "BRAID", "BRAIN", "BRAKE", "BRAND", "BRASH", "BRASS", "BRAVE", "BRAWL", "BRAWN", "BRAZE", "BREAD", "BREAK", "BREAM", "BREED", "BRIAR", "BRIBE", "BRICK", "BRIDE", "BRIEF", "BRIER", "BRINE", "BRING", "BRINK", "BRINY", "BRISK", "BROAD", "BROIL", "BROKE", "BROOK", "BROOM", "BROTH", "BROWN", "BROWS", "BRUNT", "BRUSH", "BRUTE", "BUCKS", "BUDDY", "BUDGE", "BUGGY", "BUILD", "BUILT", "BULBS", "BULGE", "BULKY", "BULLS", "BUMPY", "BUNCH", "BUNNY", "BURNS", "BURNT", "BURST", "BUSES", "BUYER", "BUZZY", "BYLAW", "BYWAY",
            "CABBY", "CABIN", "CABLE", "CACHE", "CAIRN", "CAKES", "CALLS", "CALVE", "CALYX", "CAMPS", "CAMPY", "CANAL", "CANDY", "CANED", "CANNY", "CANOE", "CANON", "CARDS", "CARED", "CARER", "CARES", "CARGO", "CAROL", "CARRY", "CARVE", "CASED", "CASES", "CASTE", "CATCH", "CATER", "CAULK", "CAUSE", "CAVES", "CEASE", "CEDED", "CELLS", "CENTS", "CHAFE", "CHAFF", "CHAIN", "CHAIR", "CHALK", "CHAMP", "CHANT", "CHAOS", "CHAPS", "CHARM", "CHART", "CHARY", "CHASE", "CHASM", "CHEAP", "CHEAT", "CHECK", "CHEEK", "CHEER", "CHEMO", "CHESS", "CHEST", "CHICK", "CHIDE", "CHIEF", "CHILD", "CHILI", "CHILL", "CHIME", "CHINA", "CHIPS", "CHOIR", "CHORD", "CHORE", "CHOSE", "CHUCK", "CHUNK", "CHUTE", "CIDER", "CIGAR", "CINCH", "CITED", "CITES", "CIVET", "CIVIC", "CIVIL", "CLADE", "CLAIM", "CLANK", "CLASH", "CLASS", "CLAWS", "CLEAN", "CLEAR", "CLEAT", "CLERK", "CLICK", "CLIFF", "CLIMB", "CLING", "CLOAK", "CLOCK", "CLONE", "CLOSE", "CLOTH", "CLOUD", "CLOUT", "CLOVE", "CLOWN", "CLUBS", "CLUCK", "CLUES", "CLUNG", "CLUNK", "COACH", "COAST", "COATS", "COCOA", "CODES", "COINS", "COLIC", "COLON", "COLOR", "COMAL", "COMES", "COMIC", "COMMA", "CONCH", "CONIC", "CORAL", "CORGI", "CORNY", "CORPS", "COSTS", "COTTA", "COUCH", "COUGH", "COULD", "COUNT", "COURT", "COVEN", "COVER", "COYLY", "CRACK", "CRAFT", "CRANE", "CRANK", "CRASH", "CRASS", "CRATE", "CRAVE", "CRAWL", "CRAZY", "CREAK", "CREAM", "CREED", "CREEK", "CREPT", "CREST", "CREWS", "CRIED", "CRIES", "CRIME", "CRISP", "CRONE", "CROPS", "CROSS", "CROWD", "CROWN", "CRUDE", "CRUEL", "CRUSH", "CRUST", "CRYPT", "CUBAN", "CUBBY", "CUBIC", "CUBIT", "CUMIN", "CURLS", "CURLY", "CURRY", "CURSE", "CURVE", "CUTIE", "CYCLE", "CYNIC", "CZECH",
            "DACHA", "DADDY", "DAILY", "DAIRY", "DAISY", "DALLY", "DANCE", "DARED", "DATED", "DATES", "DATUM", "DEALS", "DEALT", "DEATH", "DEBIT", "DEBTS", "DEBUG", "DEBUT", "DECAF", "DECAL", "DECAY", "DECOR", "DECOY", "DEEDS", "DEIST", "DEITY", "DELAY", "DELFT", "DELVE", "DEMUR", "DENIM", "DENSE", "DEPOT", "DEPTH", "DERBY", "DERRY", "DESKS", "DETER", "DETOX", "DEUCE", "DEVIL", "DIARY", "DICED", "DIETS", "DIGIT", "DIMLY", "DINAR", "DINER", "DINGY", "DIRTY", "DISCO", "DISCS", "DISKS", "DITCH", "DITTY", "DITZY", "DIVAN", "DIVED", "DIVER", "DIVOT", "DIVVY", "DIZZY", "DOCKS", "DODGE", "DODGY", "DOGGY", "DOGMA", "DOING", "DOLLS", "DOMED", "DONOR", "DONUT", "DOORS", "DORIC", "DOSED", "DOSES", "DOTTY", "DOUBT", "DOUGH", "DOUSE", "DOWNS", "DOZEN", "DRAFT", "DRAIN", "DRAMA", "DRANK", "DRAWN", "DRAWS", "DREAD", "DREAM", "DRESS", "DRIED", "DRIER", "DRIFT", "DRILL", "DRILY", "DRINK", "DRIVE", "DROLL", "DRONE", "DROPS", "DROVE", "DROWN", "DRUGS", "DRUMS", "DRUNK", "DRYER", "DUCAT", "DUCHY", "DUCKS", "DUMMY", "DUNCE", "DUNES", "DUSTY", "DUTCH", "DUVET", "DWARF", "DWELL", "DYING",
            "EAGER", "EAGLE", "EARED", "EARLY", "EARTH", "EASED", "EASEL", "EATEN", "ECLAT", "EDEMA", "EDGES", "EDICT", "EDIFY", "EERIE", "EGRET", "EIDER", "EIGHT", "ELATE", "ELBOW", "ELDER", "ELECT", "ELIDE", "ELITE", "ELUDE", "ELVES", "EMCEE", "EMOTE", "EMPTY", "ENACT", "ENDED", "ENEMA", "ENEMY", "ENJOY", "ENNUI", "ENSUE", "ENTER", "ENTRY", "ENVOY", "EQUAL", "EQUIP", "ERASE", "ERECT", "ERROR", "ESSAY", "ETHIC", "ETHOS", "ETUDE", "EVADE", "EVENT", "EVERY", "EVICT", "EXACT", "EXALT", "EXAMS", "EXERT", "EXILE", "EXIST", "EXTRA", "EXUDE",
            "FACED", "FACES", "FACTS", "FADED", "FAILS", "FAINT", "FAIRS", "FAIRY", "FAITH", "FALLS", "FALSE", "FAMED", "FANCY", "FARES", "FARMS", "FATAL", "FATED", "FATTY", "FATWA", "FAULT", "FAUNA", "FAVOR", "FEARS", "FEAST", "FECAL", "FEELS", "FEINT", "FELLA", "FENCE", "FERRY", "FETAL", "FETCH", "FEVER", "FEWER", "FIBER", "FIBRE", "FIELD", "FIERY", "FIFTH", "FIFTY", "FIGHT", "FILCH", "FILED", "FILES", "FILET", "FILLE", "FILLS", "FILLY", "FILMS", "FILMY", "FILTH", "FINAL", "FINDS", "FINED", "FINER", "FINES", "FINNY", "FIRED", "FIRES", "FIRMS", "FIRST", "FISTS", "FIVER", "FIXED", "FLAGS", "FLAIL", "FLAIR", "FLAME", "FLANK", "FLARE", "FLASH", "FLASK", "FLATS", "FLAWS", "FLEET", "FLESH", "FLIES", "FLING", "FLIRT", "FLOAT", "FLOCK", "FLOOD", "FLOOR", "FLORA", "FLOUR", "FLOUT", "FLOWN", "FLOWS", "FLUID", "FLUNG", "FLUNK", "FLUSH", "FLUTE", "FLYBY", "FOCAL", "FOCUS", "FOGGY", "FOIST", "FOLDS", "FOLIC", "FOLIO", "FOLKS", "FOLLY", "FONTS", "FOODS", "FOOLS", "FORAY", "FORCE", "FORGE", "FORGO", "FORMS", "FORTE", "FORTH", "FORTY", "FORUM", "FOUND", "FOUNT", "FOURS", "FOVEA", "FOXES", "FOYER", "FRAIL", "FRAME", "FRANC", "FRANK", "FRAUD", "FREAK", "FREED", "FRESH", "FRIED", "FRILL", "FRISK", "FROGS", "FRONT", "FROST", "FROWN", "FROZE", "FRUIT", "FUDGE", "FUELS", "FULLY", "FUMES", "FUNDS", "FUNNY", "FUSED", "FUTON", "FUZZY",
            "GAINS", "GAMES", "GANGS", "GASES", "GATES", "GAUGE", "GAZED", "GEESE", "GENES", "GENIE", "GENRE", "GENUS", "GHOST", "GHOUL", "GIANT", "GIDDY", "GIFTS", "GIMPY", "GIRLS", "GIRLY", "GIRTH", "GIVEN", "GIVES", "GIZMO", "GLAND", "GLARE", "GLASS", "GLEAM", "GLEAN", "GLIAL", "GLIDE", "GLINT", "GLOBE", "GLOOM", "GLORY", "GLOSS", "GLOVE", "GLUED", "GLUON", "GOALS", "GOATS", "GOING", "GOLLY", "GOODS", "GOOFY", "GOOPY", "GOOSE", "GORGE", "GRACE", "GRADE", "GRAFT", "GRAIN", "GRAMS", "GRAND", "GRANT", "GRAPE", "GRAPH", "GRASP", "GRASS", "GRATE", "GRAVE", "GRAVY", "GREAT", "GREED", "GREEK", "GREEN", "GREET", "GRIEF", "GRILL", "GRIME", "GRIMY", "GRIND", "GRIPS", "GROIN", "GROOM", "GROSS", "GROUP", "GROUT", "GROWN", "GROWS", "GRUEL", "GRUMP", "GRUNT", "GUANO", "GUARD", "GUAVA", "GUESS", "GUEST", "GUIDE", "GUILD", "GUILT", "GUISE", "GULLS", "GULLY", "GUMMY", "GUNKY", "GUNNY", "GUSHY", "GUSTY", "GUTSY", "GYPSY", "GYRUS",
            "HABIT", "HAIKU", "HAIRS", "HAIRY", "HALAL", "HALLS", "HALVE", "HAMMY", "HANDS", "HANDY", "HANGS", "HAPPY", "HARDY", "HAREM", "HARPY", "HARSH", "HASTE", "HASTY", "HATCH", "HATED", "HATES", "HAUNT", "HAVEN", "HAVOC", "HAZEL", "HEADS", "HEADY", "HEARD", "HEARS", "HEART", "HEATH", "HEAVE", "HEAVY", "HEDGE", "HEELS", "HEFTY", "HEIRS", "HEIST", "HELIX", "HELLO", "HELPS", "HENCE", "HENRY", "HERBS", "HERDS", "HILLS", "HILLY", "HINDU", "HINGE", "HINTS", "HIPPO", "HIRED", "HITCH", "HOBBY", "HOIST", "HOLDS", "HOLES", "HOLLY", "HOMED", "HOMES", "HONEY", "HONOR", "HOOKS", "HOPED", "HOPES", "HORNS", "HORSE", "HOSEL", "HOSTS", "HOTEL", "HOTLY", "HOUND", "HOURS", "HOUSE", "HUBBY", "HUGGY", "HULLO", "HUMAN", "HUMID", "HUMOR", "HUMUS", "HURRY", "HURTS", "HUSKY", "HYENA", "HYMNS",
            "ICHOR", "ICILY", "ICING", "ICONS", "IDEAL", "IDEAS", "IDIOM", "IDIOT", "IDLED", "IDYLL", "IGLOO", "ILIAC", "ILIUM", "IMAGE", "IMAGO", "IMBUE", "IMPLY", "INANE", "INCUS", "INDEX", "INDIA", "INDIE", "INERT", "INFER", "INFRA", "INGOT", "INLAY", "INLET", "INNER", "INPUT", "INTRO", "IRISH", "IRONY", "ISLET", "ISSUE", "ITCHY", "ITEMS", "IVORY",
            "JAPAN", "JEANS", "JELLY", "JEWEL", "JOINS", "JOINT", "JOKER", "JOKES", "JOLLY", "JOULE", "JOUST", "JUDGE", "JUICE", "JUICY", "JUMBO", "JUMPS", "JUNTA",
            "KABOB", "KANJI", "KARAT", "KARMA", "KAYAK", "KAZOO", "KEEPS", "KICKS", "KIDDO", "KILLS", "KINDA", "KINDS", "KINGS", "KITTY", "KNAVE", "KNEAD", "KNEEL", "KNEES", "KNELT", "KNIFE", "KNOBS", "KNOCK", "KNOLL", "KNOTS", "KNOWN", "KNOWS", "KOALA", "KUDOS", "KUDZU",
            "LABEL", "LABOR", "LACED", "LACKS", "LADLE", "LAGER", "LAITY", "LAKES", "LAMBS", "LAMPS", "LANDS", "LANES", "LAPIN", "LAPSE", "LARGE", "LARVA", "LASER", "LASSO", "LASTS", "LATCH", "LATER", "LATHE", "LATIN", "LATTE", "LAUGH", "LAWNS", "LAYER", "LAYUP", "LEACH", "LEADS", "LEAFY", "LEAKY", "LEANT", "LEAPT", "LEARN", "LEASE", "LEASH", "LEAST", "LEAVE", "LEDGE", "LEECH", "LEGAL", "LEGGY", "LEMMA", "LEMON", "LEMUR", "LEVEL", "LEVER", "LIANA", "LIBEL", "LIDAR", "LIEGE", "LIFTS", "LIGHT", "LIKED", "LIKEN", "LIKES", "LILAC", "LIMBO", "LIMBS", "LIMIT", "LINED", "LINEN", "LINER", "LINES", "LINGO", "LINKS", "LIONS", "LIPID", "LISTS", "LITER", "LITRE", "LIVED", "LIVEN", "LIVER", "LIVES", "LIVID", "LLAMA", "LOADS", "LOANS", "LOBBY", "LOCAL", "LOCKS", "LOCUS", "LODGE", "LOFTY", "LOGIC", "LOGIN", "LOGON", "LOLLY", "LONER", "LOOKS", "LOONY", "LOOPS", "LOOPY", "LOOSE", "LORDS", "LORRY", "LOSER", "LOSES", "LOTTO", "LOTUS", "LOUSE", "LOUSY", "LOVED", "LOVER", "LOVES", "LOWER", "LOYAL", "LUCID", "LUCKY", "LUCRE", "LUMEN", "LUMPS", "LUMPY", "LUNAR", "LUNCH", "LUNGE", "LUNGS", "LUSTY", "LYING", "LYMPH", "LYNCH", "LYRIC",
            "MACHO", "MADAM", "MADLY", "MAGIC", "MAGMA", "MAINS", "MAIZE", "MAJOR", "MAKER", "MAKES", "MALES", "MAMBO", "MANGO", "MANGY", "MANIA", "MANIC", "MANLY", "MANOR", "MAPLE", "MARCH", "MARKS", "MARRY", "MARSH", "MASKS", "MATCH", "MATED", "MATES", "MATHS", "MATTE", "MAVEN", "MAXIM", "MAYAN", "MAYBE", "MAYOR", "MEALS", "MEANS", "MEANT", "MEATY", "MEDAL", "MEDIA", "MEDIC", "MEETS", "MELON", "MENUS", "MERCY", "MERGE", "MERIT", "MERRY", "MESON", "MESSY", "METAL", "METER", "METRE", "MICRO", "MIDST", "MIGHT", "MILES", "MILLS", "MIMIC", "MINCE", "MINDS", "MINED", "MINER", "MINES", "MINOR", "MINTY", "MINUS", "MIRED", "MIRTH", "MISTY", "MITRE", "MIXED", "MIXER", "MODEL", "MODEM", "MODES", "MOGUL", "MOIST", "MOLAR", "MOLDY", "MOLES", "MONEY", "MONKS", "MONTH", "MOODS", "MOONY", "MOORS", "MOOSE", "MORAL", "MORAY", "MORPH", "MOTEL", "MOTIF", "MOTOR", "MOTTO", "MOULD", "MOUND", "MOUNT", "MOUSE", "MOUTH", "MOVED", "MOVER", "MOVES", "MOVIE", "MUCUS", "MUDDY", "MUMMY", "MUNCH", "MURKY", "MUSED", "MUSIC", "MUSTY", "MUTED", "MUZZY", "MYTHS",
            "NACHO", "NADIR", "NAILS", "NAIVE", "NAKED", "NAMED", "NAMES", "NANNY", "NASAL", "NASTY", "NATAL", "NATTY", "NAVAL", "NAVEL", "NEATH", "NECKS", "NEEDS", "NEEDY", "NEIGH", "NERVE", "NESTS", "NEVER", "NEWER", "NEWLY", "NEXUS", "NICER", "NICHE", "NIECE", "NIFTY", "NIGHT", "NINJA", "NINTH", "NITRO", "NOBLE", "NOBLY", "NODES", "NOISE", "NOISY", "NOMAD", "NOMES", "NONCE", "NOOSE", "NORMS", "NORTH", "NOSES", "NOTCH", "NOTED", "NOTES", "NOVEL", "NUDGE", "NURSE", "NUTTY", "NYLON", "NYMPH",
            "OASIS", "OCCUR", "OCEAN", "ODDLY", "ODOUR", "OFFER", "OFTEN", "OILED", "OLDER", "OLDIE", "OLIVE", "ONION", "ONSET", "OOMPH", "OPENS", "OPERA", "OPINE", "OPIUM", "OPTIC", "ORBIT", "ORDER", "ORGAN", "OTHER", "OTTER", "OUGHT", "OUNCE", "OUTDO", "OUTER", "OVERS", "OVERT", "OVOID", "OVULE", "OWNED", "OWNER", "OXBOW", "OXIDE", "OZONE",
            "PACKS", "PADDY", "PAGES", "PAINS", "PAINT", "PAIRS", "PALMS", "PANDA", "PANEL", "PANIC", "PANTS", "PAPAL", "PAPER", "PARKS", "PARTS", "PARTY", "PASTA", "PASTE", "PATCH", "PATHS", "PATIO", "PAUSE", "PEACE", "PEACH", "PEAKS", "PEARL", "PEARS", "PEDAL", "PEERS", "PENAL", "PENCE", "PENNY", "PERIL", "PESTS", "PETTY", "PHASE", "PHONE", "PHOTO", "PIANO", "PICKS", "PIECE", "PIERS", "PIGGY", "PILAF", "PILED", "PILES", "PILLS", "PILOT", "PINCH", "PINTS", "PIOUS", "PIPES", "PISTE", "PITCH", "PIVOT", "PIXEL", "PIXIE", "PIZZA", "PLACE", "PLAIN", "PLAIT", "PLANE", "PLANK", "PLANS", "PLANT", "PLATE", "PLAYS", "PLAZA", "PLEAD", "PLEAS", "PLEAT", "PLOTS", "PLUMB", "PLUME", "PLUMP", "POEMS", "POETS", "POINT", "POKER", "POLAR", "POLES", "POLIO", "POLLS", "POLYP", "PONDS", "POOLS", "PORCH", "PORES", "PORTS", "POSED", "POSES", "POSIT", "POSTS", "POUCH", "POUND", "POWER", "PREEN", "PRESS", "PRICE", "PRICY", "PRIDE", "PRIMA", "PRIME", "PRIMP", "PRINT", "PRION", "PRIOR", "PRISE", "PRISM", "PRIVY", "PRIZE", "PROBE", "PROMO", "PRONE", "PRONG", "PROOF", "PROSE", "PROUD", "PROVE", "PROXY", "PRUDE", "PRUNE", "PUDGY", "PULLS", "PULSE", "PUMPS", "PUNCH", "PUPIL", "PUPPY", "PURSE", "PYLON",
            "QUACK", "QUAIL", "QUALM", "QUARK", "QUART", "QUASI", "QUEEN", "QUELL", "QUERY", "QUEST", "QUEUE", "QUICK", "QUIET", "QUILL", "QUILT", "QUINT", "QUIRK", "QUITE", "QUOTA", "QUOTE",
            "RABBI", "RACED", "RACES", "RADAR", "RADIO", "RAGGY", "RAIDS", "RAILS", "RAINY", "RAISE", "RALLY", "RAMPS", "RANCH", "RANGE", "RANGY", "RANKS", "RAPID", "RATED", "RATES", "RATIO", "RATTY", "RAVEN", "RAZOR", "REACH", "REACT", "READS", "READY", "REALM", "REARM", "REBEL", "RECAP", "RECON", "RECTO", "REDLY", "REEDY", "REFER", "REHAB", "REIGN", "REINS", "RELAX", "RELAY", "RELIC", "REMIT", "REMIX", "RENAL", "RENEW", "RENTS", "REPAY", "REPLY", "RESIN", "RESTS", "RETRO", "REUSE", "RHINO", "RHYME", "RIDER", "RIDGE", "RIFLE", "RIGHT", "RIGID", "RIGOR", "RILED", "RINGS", "RINSE", "RIOTS", "RISEN", "RISES", "RISKS", "RISKY", "RITES", "RITZY", "RIVAL", "RIVEN", "RIVER", "RIVET", "ROADS", "ROAST", "ROBES", "ROBOT", "ROCKS", "ROCKY", "ROGUE", "ROILY", "ROLES", "ROLLS", "ROMAN", "ROOFS", "ROOMS", "ROOMY", "ROOTS", "ROPES", "ROSES", "ROSIN", "ROTOR", "ROUGE", "ROUGH", "ROUND", "ROUTE", "ROVER", "ROYAL", "RUDDY", "RUGBY", "RUINS", "RULED", "RULER", "RULES", "RUMBA", "RUMMY", "RUMOR", "RUNIC", "RUNNY", "RUNTY", "RURAL", "RUSTY",
            "SABLE", "SADLY", "SAFER", "SAGGY", "SAILS", "SAINT", "SALAD", "SALES", "SALLY", "SALON", "SALSA", "SALTS", "SALTY", "SALVE", "SAMBA", "SANDS", "SANDY", "SATED", "SATIN", "SATYR", "SAUCE", "SAUCY", "SAUNA", "SAVED", "SAVER", "SAVES", "SAVOR", "SAVVY", "SCALD", "SCALE", "SCALP", "SCALY", "SCAMP", "SCANT", "SCAPE", "SCARE", "SCARF", "SCARP", "SCARS", "SCARY", "SCENE", "SCENT", "SCHMO", "SCOFF", "SCOLD", "SCONE", "SCOOP", "SCOOT", "SCOPE", "SCORE", "SCORN", "SCOTS", "SCOUR", "SCOUT", "SCRAM", "SCRAP", "SCREW", "SCRIM", "SCRIP", "SCRUB", "SCRUM", "SCUBA", "SEALS", "SEAMS", "SEATS", "SEEDS", "SEEDY", "SEEKS", "SEEMS", "SEGUE", "SEIZE", "SELLS", "SENDS", "SENSE", "SERUM", "SERVE", "SETUP", "SEVEN", "SHADE", "SHADY", "SHAFT", "SHAKE", "SHAKY", "SHALE", "SHALL", "SHAME", "SHANK", "SHAPE", "SHARD", "SHARE", "SHARP", "SHAVE", "SHAWL", "SHEAF", "SHEAR", "SHEEN", "SHEEP", "SHEER", "SHEET", "SHELF", "SHELL", "SHIFT", "SHILL", "SHINE", "SHINY", "SHIPS", "SHIRE", "SHIRT", "SHOCK", "SHOES", "SHONE", "SHOOK", "SHOOT", "SHOPS", "SHORE", "SHORN", "SHORT", "SHOTS", "SHOUT", "SHOVE", "SHOWN", "SHOWS", "SHRUG", "SHUNT", "SHUSH", "SIDES", "SIDLE", "SIEGE", "SIGHT", "SIGIL", "SIGNS", "SILLY", "SILTY", "SINCE", "SINEW", "SINGE", "SINGS", "SINUS", "SITAR", "SITES", "SIXTH", "SIXTY", "SIZED", "SIZES", "SKALD", "SKANK", "SKATE", "SKEIN", "SKIER", "SKIES", "SKIFF", "SKILL", "SKIMP", "SKINS", "SKIRT", "SKULL", "SLABS", "SLAIN", "SLAKE", "SLANG", "SLANT", "SLASH", "SLATE", "SLAVE", "SLEEK", "SLEEP", "SLEET", "SLEPT", "SLICE", "SLIDE", "SLIME", "SLIMY", "SLING", "SLINK", "SLOPE", "SLOSH", "SLOTH", "SLOTS", "SLUMP", "SLUSH", "SLYLY", "SMALL", "SMART", "SMASH", "SMEAR", "SMELL", "SMELT", "SMILE", "SMITE", "SMOKE", "SNAIL", "SNAKE", "SNARE", "SNARL", "SNEER", "SNIDE", "SNIFF", "SNIPE", "SNOOP", "SNORE", "SNORT", "SNOUT", "SOBER", "SOCKS", "SOFTY", "SOGGY", "SOILS", "SOLAR", "SOLID", "SOLVE", "SONAR", "SONGS", "SONIC", "SOOTH", "SORRY", "SORTS", "SOUGH", "SOULS", "SOUND", "SOUTH", "SPACE", "SPADE", "SPAIN", "SPARE", "SPARK", "SPATE", "SPAWN", "SPEAK", "SPEED", "SPELL", "SPEND", "SPENT", "SPIES", "SPINE", "SPLAT", "SPLIT", "SPOIL", "SPOKE", "SPOON", "SPORT", "SPOTS", "SPRAY", "SPURS", "SQUAD", "STACK", "STAFF", "STAGE", "STAIN", "STAIR", "STAKE", "STALE", "STALL", "STAMP", "STAND", "STARE", "STARK", "STARS", "START", "STASH", "STATE", "STAYS", "STEAD", "STEAK", "STEAL", "STEAM", "STEEL", "STEEP", "STEER", "STEMS", "STENO", "STEPS", "STERN", "STICK", "STIFF", "STILE", "STILL", "STILT", "STING", "STINK", "STINT", "STOCK", "STOIC", "STOKE", "STOLE", "STOMP", "STONE", "STONY", "STOOD", "STOOL", "STOOP", "STOPS", "STORE", "STORK", "STORM", "STORY", "STOUT", "STOVE", "STRAP", "STRAW", "STRAY", "STREP", "STREW", "STRIP", "STRUM", "STRUT", "STUCK", "STUDY", "STUFF", "STUMP", "STUNT", "STYLE", "SUAVE", "SUEDE", "SUGAR", "SUITE", "SUITS", "SULLY", "SUNNY", "SUNUP", "SUPER", "SURGE", "SUSHI", "SWALE", "SWAMI", "SWAMP", "SWANK", "SWANS", "SWARD", "SWARM", "SWASH", "SWATH", "SWEAR", "SWEAT", "SWEEP", "SWEET", "SWELL", "SWEPT", "SWIFT", "SWILL", "SWINE", "SWING", "SWIPE", "SWIRL", "SWISH", "SWISS", "SWOON", "SWOOP", "SWORD", "SWORE", "SWORN", "SWUNG",
            "TABLE", "TACIT", "TAFFY", "TAILS", "TAINT", "TAKEN", "TAKES", "TALES", "TALKS", "TALLY", "TALON", "TAMED", "TANGO", "TANGY", "TANKS", "TAPES", "TARDY", "TAROT", "TARRY", "TASKS", "TASTE", "TASTY", "TATTY", "TAUNT", "TAWNY", "TAXED", "TAXES", "TAXIS", "TAXON", "TEACH", "TEAMS", "TEARS", "TEARY", "TEASE", "TECHY", "TEDDY", "TEENS", "TEENY", "TEETH", "TELLS", "TELLY", "TEMPO", "TENDS", "TENOR", "TENSE", "TENTH", "TENTS", "TERMS", "TESTS", "TEXAS", "TEXTS", "THANK", "THEFT", "THEIR", "THEME", "THERE", "THESE", "THETA", "THICK", "THIEF", "THIGH", "THINE", "THING", "THINK", "THIRD", "THONG", "THORN", "THOSE", "THREE", "THREW", "THROW", "THUMB", "TIARA", "TIBIA", "TIDAL", "TIDES", "TIGER", "TIGHT", "TILDE", "TILED", "TILES", "TILTH", "TIMED", "TIMER", "TIMES", "TIMID", "TINES", "TINNY", "TIPSY", "TIRED", "TITLE", "TOAST", "TODAY", "TOKEN", "TOMMY", "TONAL", "TONED", "TONES", "TONGS", "TONIC", "TONNE", "TOOLS", "TOONS", "TOOTH", "TOPAZ", "TOPIC", "TORCH", "TORSO", "TORTE", "TORUS", "TOTAL", "TOTEM", "TOUCH", "TOUGH", "TOURS", "TOWEL", "TOWER", "TOWNS", "TOXIC", "TOXIN", "TRACE", "TRACK", "TRACT", "TRADE", "TRAIL", "TRAIN", "TRAIT", "TRAMP", "TRAMS", "TRASH", "TRAWL", "TRAYS", "TREAD", "TREAT", "TREES", "TREND", "TRIAD", "TRIAL", "TRIBE", "TRICK", "TRIED", "TRIES", "TRIKE", "TRILL", "TRIPS", "TRITE", "TROLL", "TROOP", "TROUT", "TRUCE", "TRUCK", "TRULY", "TRUNK", "TRUST", "TRUTH", "TUBBY", "TUBES", "TULIP", "TUMMY", "TUNED", "TUNES", "TUNIC", "TURKS", "TURNS", "TUTEE", "TUTOR", "TWANG", "TWEAK", "TWICE", "TWINS", "TWIRL", "TWIST", "TYING", "TYPES", "TYRES",
            "UDDER", "ULCER", "ULNAR", "ULTRA", "UMBRA", "UNBAN", "UNCAP", "UNCLE", "UNCUT", "UNDER", "UNDUE", "UNFED", "UNFIT", "UNHIP", "UNIFY", "UNION", "UNITE", "UNITS", "UNITY", "UNLIT", "UNMET", "UNSAY", "UNTIE", "UNTIL", "UNZIP", "UPPER", "UPSET", "URBAN", "URGED", "URINE", "USAGE", "USERS", "USHER", "USING", "USUAL", "UTTER", "UVULA",
            "VAGUE", "VALET", "VALID", "VALOR", "VALUE", "VALVE", "VAPOR", "VAULT", "VAUNT", "VEINS", "VEINY", "VENAL", "VENOM", "VENUE", "VERBS", "VERGE", "VERSE", "VICAR", "VIDEO", "VIEWS", "VIGIL", "VIGOR", "VILLA", "VINES", "VINYL", "VIRAL", "VIRUS", "VISIT", "VISOR", "VITAL", "VIVID", "VIXEN", "VOCAL", "VODKA", "VOGUE", "VOICE", "VOTED", "VOTER", "VOTES", "VOUCH", "VOWED", "VOWEL", "VROOM",
            "WAGES", "WAGON", "WAIST", "WAITS", "WAIVE", "WALKS", "WALLS", "WALTZ", "WANTS", "WARDS", "WARES", "WARNS", "WASTE", "WATCH", "WATER", "WAVED", "WAVES", "WAXEN", "WEARS", "WEARY", "WEAVE", "WEBBY", "WEDGE", "WEEDS", "WEEKS", "WEIGH", "WEIRD", "WELLS", "WELSH", "WETLY", "WHALE", "WHEAT", "WHEEL", "WHERE", "WHICH", "WHILE", "WHINE", "WHISK", "WHITE", "WHOLE", "WHORL", "WHOSE", "WIDEN", "WIDER", "WIDOW", "WIDTH", "WIELD", "WILLS", "WIMPY", "WINCE", "WINCH", "WINDS", "WINDY", "WINES", "WINGS", "WIPED", "WIRED", "WIRES", "WISER", "WITCH", "WITTY", "WIVES", "WOKEN", "WOMAN", "WOMEN", "WOODS", "WORDS", "WORKS", "WORLD", "WORMS", "WORMY", "WORRY", "WORSE", "WORST", "WORTH", "WOULD", "WOUND", "WOVEN", "WRATH", "WRECK", "WRIST", "WRITE", "WRONG", "WROTE", "WRYLY",
            "XENON", "XEROX",
            "YACHT", "YARDS", "YAWNS", "YEARN", "YEARS", "YEAST", "YELLS", "YIELD", "YODEL", "YOUNG", "YOURS", "YOUTH", "YUCCA", "YUMMY",
            "ZEBRA", "ZILCH", "ZINGY", "ZONAL", "ZONES"
        };

    private string[] _wordList;

    private static readonly int[][] _key = new int[26][]
    {
        new int[5] {2, 5, 7, 5, 5},
        new int[5] {6, 5, 6, 5, 6},
        new int[5] {2, 5, 4, 5, 2},
        new int[5] {6, 5, 5, 5, 6},
        new int[5] {7, 4, 6, 4, 7},
        new int[5] {7, 4, 6, 4, 4},
        new int[5] {3, 4, 5, 5, 2},
        new int[5] {5, 5, 7, 5, 5},
        new int[5] {7, 2, 2, 2, 7},
        new int[5] {7, 1, 1, 5, 2},
        new int[5] {5, 5, 6, 5, 5},
        new int[5] {4, 4, 4, 4, 7},
        new int[5] {5, 7, 7, 5, 5},
        new int[5] {7, 5, 5, 5, 5},
        new int[5] {2, 5, 5, 5, 2},
        new int[5] {6, 5, 6, 4, 4},
        new int[5] {2, 5, 5, 2, 1},
        new int[5] {6, 5, 6, 5, 5},
        new int[5] {3, 4, 2, 1, 6},
        new int[5] {7, 2, 2, 2, 2},
        new int[5] {5, 5, 5, 5, 7},
        new int[5] {5, 5, 5, 5, 2},
        new int[5] {5, 5, 7, 7, 5},
        new int[5] {5, 5, 2, 5, 5},
        new int[5] {5, 5, 7, 2, 2},
        new int[5] {7, 1, 2, 4, 7}
    };

    private static readonly Color32[] _colors = new Color32[]
    {
        new Color32(0, 0, 0, 255),
        new Color32(0, 0, 255, 255),
        new Color32(0, 255, 0, 255),
        new Color32(0, 255, 255, 255),
        new Color32(255, 0, 0, 255),
        new Color32(255, 0, 255, 255),
        new Color32(255, 255, 0, 255),
        new Color32(255, 255, 255, 255)
    };

    private static readonly int[] _originalShift = new int[] { 0, 5, 10, 15, 20, 1, 6, 11, 16, 21, 2, 7, 12, 17, 22, 3, 8, 13, 18, 23, 4, 9, 14, 19, 24 };

    private string _word;
    private int _solutionPos;
    private const int _wordsPerBox = 21;

    private bool _isAnimating;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;

        // RULE SEED
        var rnd = RuleSeedable.GetRNG();
        var fwl = _fullWordList.ToArray();
        rnd.ShuffleFisherYates(fwl);
        _wordList = fwl.Take(25 * _wordsPerBox).ToArray();

        for (int i = 0; i < SquareSels.Length; i++)
            SquareSels[i].OnInteract += SquarePress(i);

        _word = _wordList.PickRandom();
        _solutionPos = Array.IndexOf(_wordList, _word) / _wordsPerBox;
        var arr = GenerateArray(_word);
        for (int i = 0; i < SquareObjs.Length; i++)
            SquareObjs[i].GetComponent<MeshRenderer>().material.color = _colors[arr[i]];

        Debug.LogFormat("[Smallfont #{0}] The displayed word is {1}.", _moduleId, _word);
        Debug.LogFormat("[Smallfont #{0}] Square #{1} must be pressed.", _moduleId, _solutionPos + 1);
    }

    private KMSelectable.OnInteractHandler SquarePress(int i)
    {
        return delegate ()
        {
            if (_moduleSolved || _isAnimating)
                return false;
            _isAnimating = true;
            if (i == _solutionPos)
            {
                StartCoroutine(OpenSquare(i, true));
                StartCoroutine(MoveLight(i, true));
            }
            else
            {
                StartCoroutine(OpenSquare(i, false));
                StartCoroutine(MoveLight(i, false));
            }
            return false;
        };
    }

    private IEnumerator OpenSquare(int pos, bool isSolve)
    {
        var duration = 0.3f;
        var elapsed = 0f;
        float waitTime = isSolve ? 0.25f : 1f;
        Audio.PlaySoundAtTransform("Open", SquareObjs[pos].transform);
        while (elapsed < duration)
        {
            SquareSels[pos].transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsed, 0f, 90f, duration), 0f, 0f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        SquareSels[pos].transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        yield return new WaitForSeconds(waitTime);
        elapsed = 0f;
        while (elapsed < duration)
        {
            SquareSels[pos].transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsed, 90f, 0f, duration), 0f, 0f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        SquareSels[pos].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        Audio.PlaySoundAtTransform("Close", SquareObjs[pos].transform);
    }

    private IEnumerator MoveLight(int i, bool isSolve)
    {
        yield return new WaitForSeconds(0.25f);
        var duration = 0.3f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            StatusLightObj.transform.localPosition = new Vector3(((i % 5) - 2) * 0.03f, Easing.InOutQuad(elapsed, -0.05f, 0.04f, duration), ((i / 5) - 2) * -0.03f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        StatusLightObj.transform.localPosition = new Vector3(((i % 5) - 2) * 0.03f, 0.04f, ((i / 5) - 2) * -0.03f);
        elapsed = 0f;
        float waitTime;
        float yPos;
        if (isSolve)
        {
            waitTime = 0.1f;
            duration = 0.2f;
            yPos = 0.02f;
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
            Module.HandlePass();
            _moduleSolved = true;
        }
        else
        {
            waitTime = 0.4f;
            duration = 0.4f;
            yPos = -0.05f;
            Module.HandleStrike();
        }
        yield return new WaitForSeconds(waitTime);
        while (elapsed < duration)
        {
            StatusLightObj.transform.localPosition = new Vector3(((i % 5) - 2) * 0.03f, Easing.InOutQuad(elapsed, 0.04f, yPos, duration), ((i / 5) - 2) * -0.03f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        StatusLightObj.transform.localPosition = new Vector3(((i % 5) - 2) * 0.03f, yPos, ((i / 5) - 2) * -0.03f);
        _isAnimating = false;
    }

    private int[] GenerateArray(string word)
    {
        return Enumerable.Range(0, 25).Select(i => word.Select(j => _key[j - 'A']).ToArray().SelectMany(j => j).ToArray()[_originalShift[i]]).ToArray();
    }

#pragma warning disable 0414
    private readonly string TwitchHelpMessage = "!{0} press A1 [Press square at column A, row 1] | Columns go from A-E left to right. Rows go from 1-5 top to bottom";
#pragma warning restore 0414
    
    private IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpperInvariant();
        var m = Regex.Match(command, @"^\s*(press\s+)?(?<col>[ABCDE])(?<row>[12345])\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!m.Success)
            yield break;
        yield return null;
        while (_isAnimating)
            yield return null;
        SquareSels[(m.Groups["row"].Value[0] - '0' - 1) * 5 + (m.Groups["col"].Value[0] - 'A')].OnInteract();
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        while (_isAnimating)
            yield return null;
        SquareSels[_solutionPos].OnInteract();
        while (!_moduleSolved)
            yield return true;
    }
}
