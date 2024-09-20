namespace WebApi.Common.Exceptions;

public class TechGadgetException : Exception
{
    public class Reason
    {
        public string Title { get; }
        public string ReasonMessage { get; }

        public Reason(string title, string reasonMessage)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is blank", nameof(title));
            }
            if (string.IsNullOrWhiteSpace(reasonMessage))
            {
                throw new ArgumentException("Reason is blank", nameof(reasonMessage));
            }
            Title = title.Trim();
            ReasonMessage = reasonMessage.Trim();
        }
    }

    public TechGadgetErrorCode ErrorCode { get; }

    private readonly List<Reason> _reasons;

    public TechGadgetException(Builder builder) : base(builder.ErrorCode.Title)
    {
        ErrorCode = builder.ErrorCode;
        _reasons = builder.Reasons.ToList();
    }

    public static Builder NewBuilder()
    {
        return new Builder();
    }

    public IReadOnlyList<Reason> GetReasons()
    {
        return _reasons.AsReadOnly();
    }

    public void AddReason(Reason reason)
    {
        if (reason == null)
        {
            throw new ArgumentNullException(nameof(reason), "Reason is null");
        }
        _reasons.Add(reason);
    }

    public void AddReasons(IEnumerable<Reason> reasons)
    {
        if (reasons == null)
        {
            throw new ArgumentNullException(nameof(reasons), "Reasons is null");
        }
        _reasons.AddRange(reasons);
    }

    public void AddReason(string title, string reason)
    {
        AddReason(new Reason(title, reason));
    }

    public class Builder
    {
        public TechGadgetErrorCode ErrorCode { get; private set; } = TechGadgetErrorCode.WES_00; // Default value
        public List<Reason> Reasons { get; }

        public Builder()
        {
            Reasons = new List<Reason>();
        }

        public Builder WithCode(TechGadgetErrorCode code)
        {
            ErrorCode = code;
            return this;
        }

        public Builder AddReason(Reason reason)
        {
            if (reason == null)
            {
                throw new ArgumentNullException(nameof(reason), "Reason is null");
            }
            Reasons.Add(reason);
            return this;
        }

        public Builder AddReasons(IEnumerable<Reason> reasons)
        {
            if (reasons == null)
            {
                throw new ArgumentNullException(nameof(reasons), "Reasons is null");
            }
            Reasons.AddRange(reasons);
            return this;
        }

        public Builder AddReason(string title, string reason)
        {
            return AddReason(new Reason(title, reason));
        }

        public TechGadgetException Build()
        {
            if (ErrorCode == default)
            {
                throw new InvalidOperationException("Error code must be provided");
            }
            return new TechGadgetException(this);
        }
    }
}
