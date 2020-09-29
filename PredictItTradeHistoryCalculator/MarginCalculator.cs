using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class MarginCalculator
    {

        public void Run()
        {
            var counties = GetCounties();

            var totalPrecincts = counties.Sum(c => c.Precincts);
            Console.WriteLine($"Sum:{totalPrecincts}");

            float total = 0;
            foreach(var county in counties)
            {
                var weightedTotal = county.GetMargin() * county.GetWeight(totalPrecincts);
                //Console.WriteLine($"{county.Name} - {county.GetMargin():P} {county.GetWeight(totalPrecincts):P}");
               Console.WriteLine($"{county.Name} - {weightedTotal:P}");
                total += weightedTotal;
            }

            var other = 1 - total;
            var margin = total - other;
            Console.WriteLine($"Projection:{total:P} - {other:P} margin:{margin:P}");
        }

        public List<County> GetCounties()
        {
            return new List<County>
            {
                new County { Name = "Atlantic"  , WinnerVotes = 3588    ,LoserVotes = 435   , Reporting = 0.14f      , Precincts = 151},
                //new County { Name = "Bergen"        , WinnerVotes = 0       ,LoserVotes = 0     , Reporting = 0.0f   , Precincts = 561},
                new County { Name = "Bergen"        , WinnerVotes = 83       ,LoserVotes = 16     , Reporting = 0.3f   , Precincts = 561},//made up
                new County { Name = "Burlington"    , WinnerVotes = 33020   ,LoserVotes = 3859  , Reporting = 0.56f  , Precincts = 365},
                new County { Name = "Camden"        , WinnerVotes = 44471   ,LoserVotes = 5838  , Reporting = 0.57f  , Precincts = 345},
                new County { Name = "Cape May"   , WinnerVotes =  5628  ,LoserVotes = 714   , Reporting = 0.70f      , Precincts = 127},
                new County { Name = "Cumberland"    , WinnerVotes = 4510    ,LoserVotes = 561   , Reporting = 0.37f  , Precincts = 92},
                new County { Name = "Essex"     , WinnerVotes = 62374   ,LoserVotes = 6910  , Reporting = 0.52f      , Precincts = 550},
                new County { Name = "Gloucester"    , WinnerVotes = 24638   ,LoserVotes = 3833  , Reporting = 0.71f  , Precincts = 229},
                new County { Name = "Hudson"        , WinnerVotes = 25184   ,LoserVotes = 5185  , Reporting = 0.32f  , Precincts = 454},
                new County { Name = "Hunterdon" , WinnerVotes = 3477    ,LoserVotes = 629   , Reporting = 0.29f      , Precincts = 116},
                new County { Name = "Mercer"        , WinnerVotes = 21293   ,LoserVotes = 2744  , Reporting = 0.43f  , Precincts = 243},
                new County { Name = "Middlesex" , WinnerVotes = 27793   ,LoserVotes = 4179  , Reporting = 0.33f      , Precincts = 615},
                new County { Name = "Monmouth"  , WinnerVotes = 19271   ,LoserVotes = 2706  , Reporting = 0.33f      , Precincts = 458},
                new County { Name = "Morris"        , WinnerVotes = 13738   ,LoserVotes = 2407  , Reporting = 0.31f  , Precincts = 396},
                new County { Name = "Ocean"     , WinnerVotes = 28961   ,LoserVotes = 4222  , Reporting = 0.75f      , Precincts = 413},
                new County { Name = "Passaic"    , WinnerVotes = 7752   ,LoserVotes = 1558  , Reporting = 0.17f      , Precincts = 284},
                new County { Name = "Salem"     , WinnerVotes = 4299    ,LoserVotes = 707   , Reporting = 0.80f      , Precincts = 45},
                new County { Name = "Somerset"  , WinnerVotes = 16212   ,LoserVotes = 2504  , Reporting = 0.46f      , Precincts = 265},
                new County { Name = "Sussex"        , WinnerVotes = 6404    ,LoserVotes = 1655  , Reporting = 0.70f  , Precincts = 119},
                new County { Name = "Union"     , WinnerVotes = 20932   ,LoserVotes = 2501  , Reporting = 0.30f      , Precincts = 431},
                new County { Name = "Warren"        , WinnerVotes = 1751    ,LoserVotes = 392   , Reporting = 0.25f  , Precincts = 89},
                
            };
        }
    }

    public class County
    {
        public string Name { get; set; }
        public int ReportedPrecints { get; set; }    
        public int TotalPrecincts { get; set; }    
        public float Reporting { get; set; }
        public int WinnerVotes { get; set; }
        public int LoserVotes { get; set; }
        public int Precincts { get; set; }

        public float GetMargin()
        {
            var margin = (float)WinnerVotes / (WinnerVotes + LoserVotes);
            return margin;
        }

        public float GetWeight(int totalPrecincts)
        {
            return (float)Precincts / totalPrecincts;
        }
    }
}

//new County { Name = "Essex"	    , WinnerVotes = 62374	,LoserVotes = 6910	, Reporting = 0.52      },
//new County { Name = "Camden"	    , WinnerVotes = 44471	,LoserVotes = 5838	, Reporting = 0.57  },
//new County { Name = "Burlington"	, WinnerVotes = 33020	,LoserVotes = 3859	, Reporting = 0.56  },
//new County { Name = "Ocean"	    , WinnerVotes = 28961	,LoserVotes = 4222	, Reporting = 0.75      },
//new County { Name = "Middlesex"	, WinnerVotes = 27793	,LoserVotes = 4179	, Reporting = 0.33      },
//new County { Name = "Hudson"	    , WinnerVotes = 25184	,LoserVotes = 5185	, Reporting = 0.32  },
//new County { Name = "Gloucester"	, WinnerVotes = 24638	,LoserVotes = 3833	, Reporting = 0.71  },
//new County { Name = "Mercer"	    , WinnerVotes = 21293	,LoserVotes = 2744	, Reporting = 0.43  },
//new County { Name = "Union"	    , WinnerVotes = 20932	,LoserVotes = 2501	, Reporting = 0.30      },
//new County { Name = "Monmouth"	, WinnerVotes = 19271	,LoserVotes = 2706	, Reporting = 0.33      },
//new County { Name = "Somerset"	, WinnerVotes = 16212	,LoserVotes = 2504	, Reporting = 0.46      },
//new County { Name = "Morris"	    , WinnerVotes = 13738	,LoserVotes = 2407	, Reporting = 0.31  },
//new County { Name = "Passaic"    , WinnerVotes = 7752	,LoserVotes = 1558	, Reporting = 0.17      },
//new County { Name = "Sussex"	    , WinnerVotes = 6404	,LoserVotes = 1655	, Reporting = 0.70  },
//new County { Name = "Cape May"   , WinnerVotes =  5628	,LoserVotes = 714	, Reporting = 0.70      },
//new County { Name = "Cumberland"	, WinnerVotes = 4510	,LoserVotes = 561	, Reporting = 0.37  },
//new County { Name = "Salem"	    , WinnerVotes = 4299	,LoserVotes = 707	, Reporting = 0.80      },
//new County { Name = "Hunterdon"	, WinnerVotes = 3477	,LoserVotes = 629	, Reporting = 0.29      },
//new County { Name = "Atlantic"	, WinnerVotes = 3588	,LoserVotes = 435	, Reporting = 0.14      },
//new County { Name = "Warren"	    , WinnerVotes = 1751	,LoserVotes = 392	, Reporting = 0.25  },
//new County { Name = "Bergen"	    , WinnerVotes = 0	    ,LoserVotes = 0	    , Reporting = 0.0   },





















