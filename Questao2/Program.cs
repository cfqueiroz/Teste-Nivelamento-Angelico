using Newtonsoft.Json;
using Questao2.RestModel.Response;

public class Program
{
    public const string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches?";
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        return GetGoals(team, year);
    }


    private static int GetGoals(string team, int year)
    {
        var goalsTeam1 = GetGoalsHome(team, year);
        var goalsTeam2 = GetGoalsOut(team, year);
        return goalsTeam1 + goalsTeam2;
    }

    private static int GetGoalsOut(string team, int year)
    {
        return GetGoals(team, year, false).Result;
    }

    private static int GetGoalsHome(string team, int year)
    {
        return GetGoals(team, year, true).Result;
    }

    private static async Task<int> GetGoals(string team, int year, bool isHome)
    {
        var totalPages = 0;
        var goals = 0;
        var page = 1;
        do
        {
            HttpResponseMessage response = await Request(team, year, page, isHome);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var footMatches = JsonConvert.DeserializeObject<FootballMatches>(content);
            if (!footMatches.data.Any())
                break;
            totalPages = footMatches.total_pages;
            page = footMatches.page + 1;
            if(isHome)
                goals += footMatches.data.Sum(x => x.team1goals);
            else
                goals += footMatches.data.Sum(x => x.team2goals);
        }
        while (page <= totalPages);
        return goals;
    }


    private static async Task<HttpResponseMessage> Request(string team, int year, int page, bool isHome)
    {
        var url = baseUrl + $"year={year}&page={page}";

        if (isHome)
            url += $"&team1={team}";
        else
            url += $"&team2={team}";

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);
        return response;
    }
}