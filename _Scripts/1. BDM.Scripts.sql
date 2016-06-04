select * from Region
select * from City
select * from Area

-- vwCities
select c.*
, r.RegionName
From City c 
Inner Join Region r On r.SiteCode = c.SiteCode AND r.RegionCode = c.RegionCode

-- vwAreas
select a.*
, c.CityName
, r.RegionName
From Area a 
Inner Join City c ON a.SiteCode = c.SiteCode AND a.CityCode = c.CityCode
Inner Join Region r On r.SiteCode = a.SiteCode AND r.RegionCode = c.RegionCode

select * from vwAreas

-- vwEntities
select ety.*
, etyP.EntityName 'EntityNameParent'
, etp.EntityTypeDesc
,adr.AddressLine1, adr.AddressLine2
, adr.RegionCode, adr.RegionName, adr.CityCode, adr.CityName, adr.AreaCode, adr.AreaName
, adr.StateCode, adr.StateName, adr.CountryCode, adr.CountryName
, cnt.Mobile1, cnt.Mobile2, cnt.Phone1, cnt.Phone2
From Entity ety
Inner Join EntityType etp ON etp.SiteCode = ety.SiteCode AND etp.EntityTypeCode = ety.EntityTypeCode 
Left Outer Join Contact cnt ON cnt.SiteCode = ety.SiteCode AND cnt.EntityID = ety.ID
Left outer Join [Address] adr ON ety.SiteCode = adr.SiteCode AND ety.ID = adr.EntityID
Left Outer Join Entity etyP ON ety.SiteCode = etyP.SiteCode AND ety.ParentEntityID = etyP.ID

select * from vwEntities

select * from Region
select * from Manager
select * from Entity

SELECT 
    [Extent1].[SiteCode] AS [SiteCode], 
    [Extent1].[ID] AS [ID], 
    [Extent1].[EntityName] AS [EntityName], 
    [Extent1].[EntityTypeCode] AS [EntityTypeCode], 
    [Extent1].[IsActive] AS [IsActive]
    FROM [dbo].[Entity] AS [Extent1]

	update Entity Set ParentEntityID = 0 where ParentEntityID IS NULL

	delete from Entity where ID In (5,6)
	
	select * from Address
	select * from Contact






