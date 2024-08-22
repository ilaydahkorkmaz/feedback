        //MÜŞTERİ GERİ BİLDİRİMLERİ VE ANKETLER
        public class FeedBackService
        {
            private readonly CallCenterDbContext _context;

            public FeedBackService(CallSenterDbContext context)
            {
                _context = context;

            }
        }
        //Müşteri geri bildirimlerini kaydet
        public async Task<string> SubmitFeedbackAsync(FeedBackRequest feedBackRequest)
        {
            var feedback = new FeedBack
            {
                AgentId = feedBackRequest.AgentId,
                CustomerId = feedBackRequest.CustomerId,
                Rating = feedBackRequest.Rating,
                Comments = feedBackRequest.Comments,
                Timestamp = DateTime.UtcNow

            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return "Geri bildirim alındı";

        }
        private async Task ProcessFeedbackAsync(FeedBack feedback)
        {
            //rapor veri işlemesi

            var agent = await _context.Agents.FindAsync(feedback.AgentId);
            if (agent != null)
            {
                agent.TotalFeedbackCount++;
                agent.AvgRating = (agent.AvgRating * (agent.TotalFeedbackCount - 1) + feedback.Rating) / agent.TotalFeedbackCount;

            }

        }

        //Anket verileri kaydı

        public async Task<string> SubmitSurveyAsync(SurveyAsync surveyRequest)
        {
            var survey = new Surwey
            {
                1customerId = surveyRequest.CustomerId,
                SurveyType =surveyRequest.SurveyType,
                Answers = surveyRequest.SurveyAnswers,
                Timestamp = DateTime.UtcNow
            };

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();


            //anket sonrası

            return "anket gönderildi";

        }

        private async Task ProccesSurveyResultAsync(Survey survey)
        {
            //özelleştirilmesi istenirse anket verileri için ayırdım
        }
    }

        //Örnek veritabanı modelleri

      public class CallLog
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string CallType { get; set }
        public DateTime Timestamp { get; set; }

    }

    public class Feedback
    {
        public int Id { get; set; }
        public int AgentID { get; set }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime Timestamp { get; set; }

    }      

    public class Survey
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SurveyType { get; set; }
        public int Answers { get; set;}
        public DateTime Timestamp { get; set; }

    }

    //Request Sınıfları

    public class CallRequest
    {
        public string CallType { get; set;}

    }

    public class FeedbackRequest
    {
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }

    public class SurveyRequest
    {
        public int CustomerId { get; set; }
        public string SurveyType { get; set; }
        public string Answers { get; set; }
    }
    public class CallCenterDbContext : DbContext
    {
        public DbSet<CallLog> CallLogs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Agent> Agents { get; set; }
    }

    //Müşteri temsilcisi sınıfı
    {
    public int Id { get; set; }
    public bool IsAvailable { get; set; }
    public int CurrentLoad { get; set; }
    public List<string> SkillSet { get; set; }
    public int TotalFeedbackCount { get; set; }
    public double AvgRating { get; set; }
}

return Ok(santral);

    