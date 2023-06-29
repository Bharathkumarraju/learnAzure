SET IDENTITY_INSERT [Lookup] ON
GO

DECLARE @now DateTime2;
SELECT @now = GETUTCDATE();

DECLARE @me nvarchar(max);
SELECT @me = '(setup)';
MERGE INTO [Lookup] AS Target
USING (
	VALUES
(
		1, 
		'System.Lookup.Types', 
		'System.Lookup.Types',
		'Lookup Types',
		10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		2, 
		'System.Lookup.Types', 
		'System.Lookup.StatusValues',
		'Status Values',
		20, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		3, 
		'System.Lookup.StatusValues', 
		'Active',
		'Active',
		10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		4, 
		'System.Lookup.StatusValues', 
		'Inactive',
		'Inactive',
		20, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		6, 
		'System.Lookup.Types', 
		'System.Feedback.FeedbackType',
		'Feedback Types',
		40, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		7, 
		'System.Lookup.Types', 
		'System.Feedback.SentimentType',
		'Sentiment Types',
		50, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		8, 
		'System.Lookup.Types', 
		'System.UserRecipeTag.TagType',
		'User Recipe Tag Types',
		60, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		9, 
		'System.Lookup.Types', 
		'System.UserClaim.ClaimLogicTypes',
		'Claim Logic Types',
		70, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		400, 
		'System.Feedback.FeedbackType', 
		'Comment',
		'Comment',
		0, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		401, 
		'System.Feedback.FeedbackType', 
		'Question',
		'Question',
		10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		500, 
		'System.Feedback.SentimentType', 
		'-3',
		'Three Thumbs Down',
		-30, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		501, 
		'System.Feedback.SentimentType', 
		'-2',
		'Two Thumbs Down',
		-20, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		502, 
		'System.Feedback.SentimentType', 
		'-1',
		'One Thumb Down',
		-10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		503, 
		'System.Feedback.SentimentType', 
		'0',
		'Meh / Shrug',
		0, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		504, 
		'System.Feedback.SentimentType', 
		'1',
		'One Thumb Up',
		10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		505, 
		'System.Feedback.SentimentType', 
		'2',
		'Two Thumbs Up',
		20, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		506, 
		'System.Feedback.SentimentType', 
		'3',
		'Three Thumbs Up',
		30, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		507, 
		'System.Feedback.SentimentType', 
		'4',
		'Who has four thumbs and is happy?  (Me!)',
		40, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		700, 
		'System.UserClaim.ClaimLogicTypes', 
		'DEFAULT',
		'Default',
		0, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
),

(
		701, 
		'System.UserClaim.ClaimLogicTypes', 
		'TIME-BASED',
		'Date/Time Based',
		10, 
		'ACTIVE', 
		@me, @now, 
		@me, @now	
))

AS Source (Id, LookupType, LookupKey, LookupValue, DisplayOrder, Status, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
ON Target.Id = Source.Id
WHEN MATCHED THEN UPDATE SET
	LookupType = Source.LookupType,
	LookupKey = Source.LookupKey,
	LookupValue = Source.LookupValue,
	DisplayOrder = Source.DisplayOrder,
	Status = Source.Status,
	CreatedBy = Source.CreatedBy,
	CreatedDate = Source.CreatedDate,
	LastModifiedBy = Source.LastModifiedBy,
	LastModifiedDate = Source.LastModifiedDate
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, LookupType, LookupKey, LookupValue, DisplayOrder, Status, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
	VALUES (Id, LookupType, LookupKey, LookupValue, DisplayOrder, Status, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;
GO
