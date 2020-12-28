--Part 1
EmployerId int
Id int
Name longtext

--Part 2
SELECT Name
FROM Employers
WHERE Location = "St. Louis City";

--Part 3
SELECT DISTINCT Name, Description FROM Skills
LEFT JOIN JobSkills ON Skills.Id = JobSkills.SkillId
WHERE JobSkills.JobId IS NOT NULL
ORDER BY Skills.Name ASC;
