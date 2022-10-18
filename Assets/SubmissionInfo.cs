
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-tryhards";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "TRYHARDS";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Andrew De Leonardis", " andrew.deleonardis@student.unimelb.edu.au"),
        new TeamMember("Rajneesh Gokool", "rgokool@student.unimelb.edu.au"),
        new TeamMember("Natasha Chiorsac", "nchiorsac@student.unimelb.edu.au"),
        new TeamMember("Meaghan Gaunt", "mgaunt@student.unimelb.edu.au"), 
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "Labyrinth";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"The Minotaur is a mythical creature of great might, who had a labyrinth created around it just to contain its power. Every year the king sends nine of his greatest warriors to try and defeat the unstoppable beast and after forty winters, no mortal has yet had the power to defeat it. 

Theseus, son of Aegeus and the undefeated champion of Greece, has taken upon himself the challenge of defeating the Minotaur. Our courageous champion has decided to face the beast alone. 

Traverse the labyrinth as Theseus, collect items and fight monsters, and delve deeper into the brink to finally kill the accursed beast.

";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=yF3VpZbEH1k";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
