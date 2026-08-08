using System.Collections.Generic;
using Models.Enums;

public class BrowseFilterViewModel
{
    public string? SearchTerm { get; set; }
    public SkillCategory? Category { get; set; }
    public SkillType? Type { get; set; }
    public ProficiencyLevel? Proficiency { get; set; }
    public int Page { get; set; } = 1;
}

public class BrowseResultViewModel
{
    public int UserSkillId { get; set; }
    public string SkillName { get; set; }
    public SkillCategory Category { get; set; }
    public SkillType Type { get; set; }
    public ProficiencyLevel Proficiency { get; set; }
    public string? AdditionalInfo { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; }
    public string? UserProfilePicturePath { get; set; }
    public string? UserLocation { get; set; }
}

public class BrowseIndexViewModel
{
    public BrowseFilterViewModel Filter { get; set; }
    public List<BrowseResultViewModel> Results { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}