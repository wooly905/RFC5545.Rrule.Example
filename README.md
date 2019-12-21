# RFC5545.Rrule.Example
A quick example of recurrence rule - partial implementation of RFC 5545

## Database AvailableDateTimes table
|Column Name|Data Type|Comment    |
|---        |---      |---        |
|Id         |int      |primary key|
|StartDate  |Date     |the start date of interval that rrule applies|
|EndDate    |Date     |the end date of interval that rrule applies  |
|StartTime  |Time     |the start time in a day which user is available|
|EndTime    |Time     |the end time in a day which user is available|
|Rrule      |nvarchar(1000)|contains the rrule applies to the record - https://icalendar.org/RFC-Specifications/iCalendar-RFC-5545|

## Requirement
1. Write a web api to provide a functionality that return all time intervals of user availability by entering an date.
2. If there are several availabilities that include the given date, the availabilities with the smallest duration take precedence.
3. The duration of an availability is defined as number of days between StartDate and EndDate.
4. For the same duration you return a reunion of hour intervals for each day
5. Add a caching layer that will temporarily save the database data in memory

## Examples
- The records in AvailableDateTimes tables

|Id|StartDate|EndDate|StartTime|EndTime|Rrule|
|--|---      |---    |---      |---    |---  |
|1 |2019.1.1 |2019.12.31|10:00|13:00|FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,TU,TH|
|2 |2019.1.1 |2019.12.31|12:00|16:00|FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=FR|
|3 |2019.1.1 |2019.12.31|14:00|17:00|FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=TH|
|4 |2019.7.1 |2019.7.31 |10:00|19:00|FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,TU|
|5 |2019.7.18|2019.7.18 |15:00|19:00|FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=TH|

- Inputs and outputs

|API Input|API Output|
|----     |----      |
|2019.6.19|[]        |
|2019.6.20|[10:00-13:00, 14:00-17:00]|
|2019.6.21|[12:00-16:00]|
|2019.7.8 |[10:00-19:00]|
|2019.7.13|[]        |
|2019.7.18|[15:00-19:00]|
