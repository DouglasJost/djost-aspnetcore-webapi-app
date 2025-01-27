
-- 
-- Delete then Insert Seed Data
--
USE MusicCollectionDB;
GO

/*

select * from Genre order by Name 


select * from Artist order by LastName, FirstName 


select * from Band 


SELECT Band.BandId, Band.Name,
       BandMembership.BandMembershipId, BandMembership.StartDate, BandMembership.EndDate,
	   Artist.ArtistId, Artist.FirstName, Artist.LastName, Artist.Birthdate, Artist.Deathdate, Artist.City, Artist.Country, Artist.Instrument

  FROM Band INNER JOIN BandMembership on Band.BandId = BandMembership.BandId
            INNER JOIN Artist on Artist.ArtistId = BandMembership.ArtistId 

  ORDER BY Band.Name, Artist.LastName, Artist.FirstName


select * from Album 


select * from Song order by AlbumId, TrackNumber 


select * from SongWriter 
*/


BEGIN TRANSACTION;

BEGIN TRY
    PRINT '01 Checking if SongWriter entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM SongWriter)
    BEGIN
        DELETE FROM SongWriter;
        PRINT 'SongWriter entries deleted.'
    END;

    PRINT '02 Checking if BandMembership entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM BandMembership)
    BEGIN
        DELETE FROM BandMembership;
        PRINT 'BandMembership entries deleted.'
    END;

    PRINT '03 Checking if Song entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Song)
    BEGIN
        DELETE FROM Song;
        PRINT 'Song entries deleted.'
    END;

    PRINT '04 Checking if Album entries need to be deleted.';
    IF EXISTS (SELECT 1 FROM Album)
    BEGIN
        DELETE FROM Album;
        PRINT 'Album entries deleted.'
    END;

    PRINT '05 Checking if Band entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Band)
    BEGIN
        DELETE FROM Band;
        PRINT 'Band entries deleted.'
    END;

    PRINT '06 Checking if Artist entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Artist)
    BEGIN
        DELETE FROM Artist;
        PRINT 'Artist entries deleted.'
    END;

    PRINT '07 Checking if Genre entries need to be deleted.'
    IF EXISTS (SELECT 1 FROM Genre)
    BEGIN
        DELETE FROM Genre;
        PRINT 'Genre entries deleted.'
    END;


    PRINT '08 Inserting Genre entries'
    INSERT INTO Genre (GenreId, Name) VALUES ('9F5299A1-4C1F-45D1-BEEE-7B532E274A74', 'Blues')
    INSERT INTO Genre (GenreId, Name) VALUES ('FC5D4E1B-79A6-4BF7-A884-B987AA45FAF2', 'Classical')
    INSERT INTO Genre (GenreId, Name) VALUES ('5450B5FB-CB32-445F-9408-CD5D61F25017', 'Electronic')
    INSERT INTO Genre (GenreId, Name) VALUES ('8621AFD4-5807-4D2C-B259-F5318E09F389', 'Folk, World, & Country')
    INSERT INTO Genre (GenreId, Name) VALUES ('BBEBE87B-B839-483A-9A85-3C1A2AC39036', 'Funk / Soul')
    INSERT INTO Genre (GenreId, Name) VALUES ('ECAB3D74-68C7-4022-ABB2-263429EDC702', 'Hip-Hop')
    INSERT INTO Genre (GenreId, Name) VALUES ('9CED3D90-FA72-420D-BB03-F8F0095C37BB', 'Jazz')
    INSERT INTO Genre (GenreId, Name) VALUES ('0C96B69A-6D5C-4143-8EDF-1F5BBD0203C9', 'Latin')
    INSERT INTO Genre (GenreId, Name) VALUES ('2B789E2D-25EF-4B3F-A8F0-CC2D356EAE32', 'Non-Music')
    INSERT INTO Genre (GenreId, Name) VALUES ('E6C5AD00-F402-47E3-8630-A77A41BEC5B1', 'Pop')
    INSERT INTO Genre (GenreId, Name) VALUES ('E53193E5-E042-4033-9DA2-4C84F4A85C2F', 'Reggae')
    INSERT INTO Genre (GenreId, Name) VALUES ('076A3B53-19A4-4F57-BE32-6DBC802D9DB9', 'Rock')
    INSERT INTO Genre (GenreId, Name) VALUES ('211CCC44-0297-40F8-BC98-7815C969E248', 'Stage & Screen')
    INSERT INTO Genre (GenreId, Name) VALUES ('AC8ADC71-2195-4B41-A5EC-45C6086FAA5E', 'Psychedelia')

    PRINT '09 Artist entries'
	-- The Beatles
	-- -----------
	-- The Beatles: John Lennon
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '49C233C2-AF63-4682-AA6B-E3280432F123',
    'John',     
    'Lennon', 
    '1940-10-09', 
    '1980-12-08', 
    'Liverpool', 
    'England', 
    'vocals, guitar, keyboards, harmonica')

	-- The Beatles: Paul McCartney
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    'AAF779A0-D5D4-451C-9910-6C26D937D791', 
    'Paul', 
    'McCartney', 
    '1942-06-18', 
    null, 
    'Liverpool', 
    'England', 
    'vocals, bass, guitar, keyboards, drums')

	-- The Beatles: George Harrison
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '7120C1AF-A733-4D9E-B281-814F53B90421', 
    'George', 
    'Harrison', 
    '1943-02-25', 
    '2001-11-29', 
    'Liverpool', 
    'England', 
    'vocals, guitar, sitar, keyboards')

	-- The Beatles: Ringo Starr
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    'CAEBA01B-2D65-453E-874A-F5F67292B467', 
    'Ringo', 
    'Starr', 
    '1940-07-07', 
    null, 
    'Liverpool', 
    'England', 
    'vocals, drums')

	-- The Doors
	-- ---------
	-- The Doors: John Densmore
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    'FBE90756-59A5-4247-A7E6-D38957F5A123', 
    'John', 
    'Densmore', 
    '1944-12-01', 
    null, 
    'Los Angeles, California', 
    'USA', 
    'drums')

	-- The Doors: Robby Krieger
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '84D9E881-2D33-435D-B664-F3BFEF09AA9F', 
    'Robby', 
    'Krieger', 
    '1946-01-08', 
    null, 
    'Los Angeles, California', 
    'USA', 
    'guitar, vocals')

	-- The Doors: Ray Manzarek
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    'B82028D8-3495-494F-B075-157051250EF7', 
    'Ray', 
    'Manzarek', 
    '1939-02-12', 
    '2013-05-20', 
    'Chicago, Illinois', 
    'USA', 
    'keyboards, organ, vocals')

	-- The Doors: Jim Morrison
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '585A29A2-05F6-42F5-9745-E07C148F5FCD', 
    'Jim', 
    'Morrison', 
    '1943-12-08', 
    '1971-07-03', 
    'Melbourne, Florida,', 
    'USA', 
    'vocals')

	-- The Zombies
	-- -----------
	-- The Zombies: Rod Argent
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '8F148190-EB73-4330-8A46-7C3E261B5039', 
    'Rod', 
    'Argent', 
    null, 
    null, 
    null, 
    null, 
    'Keyboards, vocals')

	-- The Zombies: Paul Arnold
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '134A5BA1-C8D1-48B6-9ADD-613B9622B3DD', 
    'Paul', 
    'Arnold', 
    null, 
    null, 
    null, 
    null, 
    'Bass')

	-- The Zombies: Paul Atkinson
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '03BE2F9F-7113-4139-AEB2-550B35A1EDB1', 
    'Paul', 
    'Atkinson', 
    null, 
    null, 
    null, 
    null, 
    'Guitars')

	-- The Zombies: Colin Blunstone
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '81BE26E3-F0DD-4070-925E-AB9DD2D6FD15', 
    'Colin', 
    'Blunstone', 
    null, 
    null, 
    null, 
    null, 
    'Lead vocals')

	-- The Zombies: Hugh Grundy
    INSERT INTO Artist (ArtistId, FirstName, LastName, Birthdate, Deathdate, City, Country, Instrument)
    VALUES (
    '14AA5D5F-B44A-4AE1-BE47-71772E71846C', 
    'Hugh', 
    'Grundy', 
    null, 
    null, 
    null, 
    null, 
    'Drums')

    PRINT '10 Band entries'
	-- The Beatles
	-- -----------
    INSERT INTO Band (BandId, Name, FormationDate, DisbandDate, City, Country)
    VALUES (
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',
    'The Beatles',     
    '1962-01-01', 
    '1970-01-01', 
    'Liverpool', 
    'England')

	-- The Doors
	-- -----------
    INSERT INTO Band (BandId, Name, FormationDate, DisbandDate, City, Country)
    VALUES (
    'AA572312-6352-4D79-986D-242107475F94',
    'The Doors',     
    '1965-01-01', 
    '1973-08-30', 
    'Los Angeles, California', 
    'USA')

	-- The Zombies
	-- -----------
    INSERT INTO Band (BandId, Name, FormationDate, DisbandDate, City, Country)
    VALUES (
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',
    'The Zombies',     
    '1961-01-01', 
    '2024-08-30', 
    'St Albans, Hertfordshire', 
    'England')

    PRINT '10 Band Membership entries'
	-- The Beatles
	-- -----------
    -- The Beatles, John Lennon
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    'CAFC8393-52DF-4728-8B95-569D8939C298',   -- BandMembershipId
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',   -- BandId
    '49C233C2-AF63-4682-AA6B-E3280432F123',   -- ArtistId
    '1960',                                   -- StartDate 
    '1970'                                    -- EndDate
    )

    -- The Beatles, Paul McCartney
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    'DDBDACE1-A619-4E22-94F6-48A0979721A4',   -- BandMembershipId
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',   -- BandId
    'AAF779A0-D5D4-451C-9910-6C26D937D791',   -- ArtistId
    '1960',                                   -- StartDate 
    '1970'                                    -- EndDate
    )

    -- The Beatles, George Harrison
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '3EFB688A-E7D1-441D-97C1-CBA973F0DAFE',   -- BandMembershipId
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',   -- BandId
    '7120C1AF-A733-4D9E-B281-814F53B90421',   -- ArtistId
    '1960',                                   -- StartDate 
    '1970'                                    -- EndDate
    )

    -- The Beatles, Ringo Starr
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '810E2676-EFAB-4167-A11B-392055FAC4AA',   -- BandMembershipId
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',   -- BandId
    'CAEBA01B-2D65-453E-874A-F5F67292B467',   -- ArtistId
    '1960',                                   -- StartDate 
    '1970'                                    -- EndDate
    )

	-- The Doors
	-- ---------
    -- The Doors, John Densmore
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '010C4E96-334A-4F4F-8277-E10D4E17BBB4',   -- BandMembershipId
    'AA572312-6352-4D79-986D-242107475F94',   -- BandId
    'FBE90756-59A5-4247-A7E6-D38957F5A123',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Doors, Robby Krieger
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    'DAA5C7C4-C0AF-4D03-AB3E-C8119A517D0E',   -- BandMembershipId
    'AA572312-6352-4D79-986D-242107475F94',   -- BandId
    '84D9E881-2D33-435D-B664-F3BFEF09AA9F',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Doors, Ray Manzarek
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '519D93A1-9CF4-4174-9041-031F72539B99',   -- BandMembershipId
    'AA572312-6352-4D79-986D-242107475F94',   -- BandId
    'B82028D8-3495-494F-B075-157051250EF7',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Doors, Jim Morrison
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '85234F58-8D96-4F4B-B15B-13FD29E2FB1A',   -- BandMembershipId
    'AA572312-6352-4D79-986D-242107475F94',   -- BandId
    '585A29A2-05F6-42F5-9745-E07C148F5FCD',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

	-- The Zombies 
	-- -----------
    -- The Zombies, Rod Argent
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '283BC6B7-88D1-4D85-BD27-E4AE757E34EB',   -- BandMembershipId
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',   -- BandId
    '8F148190-EB73-4330-8A46-7C3E261B5039',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Zombies, Paul Arnold
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '7B214D43-F57D-4909-B763-39E31EBE007D',   -- BandMembershipId
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',   -- BandId
    '134A5BA1-C8D1-48B6-9ADD-613B9622B3DD',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Zombies, Paul Atkinson
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    'A67F0A76-56C6-4818-81AB-C96BF50E3B42',   -- BandMembershipId
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',   -- BandId
    '03BE2F9F-7113-4139-AEB2-550B35A1EDB1',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Zombies, Colin Blunstone
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    'E9D69C9D-6A4D-412B-816A-73AF3934854D',   -- BandMembershipId
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',   -- BandId
    '81BE26E3-F0DD-4070-925E-AB9DD2D6FD15',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    -- The Zombies, Hugh Grundy
    INSERT INTO BandMembership (BandMembershipId, BandId, ArtistId, StartDate, EndDate)
    VALUES (
    '7FF83B44-7045-4521-889E-8C17987C90FD',   -- BandMembershipId
    'BD737FD0-2DB5-4B41-B9EE-8A1E0BDFFF4B',   -- BandId
    '14AA5D5F-B44A-4AE1-BE47-71772E71846C',   -- ArtistId
    null,                                     -- StartDate 
    null                                      -- EndDate
    )

    PRINT '11 Album entries'
	-- The Beatles, A Hard Days Night
	-- ------------------------------
    INSERT INTO Album (AlbumId, BandId, ArtistId, Title, RecordingLabel, PlaybackFormat, Country, ReleaseDate, GenreId)
    VALUES (
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    '48787F67-9A22-4B99-BCAB-AA203D39EDAA',
    null,
    'A Hard Day''s Night',
    'Parlophone - PMC 1230',
    'vinyl, mono, LP, album',
    'UK',
    '1964-07-10',
    '076A3B53-19A4-4F57-BE32-6DBC802D9DB9'
    )

	-- The Doors, The Doors
	-- --------------------
    INSERT INTO Album (AlbumId, BandId, ArtistId, Title, RecordingLabel, PlaybackFormat, Country, ReleaseDate, GenreId)
    VALUES (
    'F8397979-CD20-44A2-9B5B-61E29FA93DF9',
    'AA572312-6352-4D79-986D-242107475F94',
    null,
    'The Doors',
    'Elektra, EKS-74007',
    'Vinyl, Repress, Stereo, Monarch Pressing',
    'USA',
    '1967-01-04',
    '076A3B53-19A4-4F57-BE32-6DBC802D9DB9'
    )

    PRINT '12 Song entries'
    -- The Beatles, A Hard Days Night
	-- ------------------------------
	-- A Hard Days Night
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '1AA4333F-A00C-4EC8-A0B3-50E9E0C11FA4',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'A Hard Day''s Night',
    'A1',
    '2:34',
    'Lead Vocals - Lennon with McCartney'
    )

	-- I Should Have Known Better
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    'C2EED7A8-1C81-4026-9116-549841979A69',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'I Should Have Known Better',
    'A2',
    '2:43',
    'Lead Vocals - Lennon'
    )

	-- If I Fell
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '6BC56A5C-E3EA-4AC0-90C3-D0B23BB1DD42',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'If I Fell',
    'A3',
    '2:19',
    'Lead Vocals - Lennon and McCartney'
    )

	-- The Beatles, I'm Happy Just to Dance With You
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    'DEA36910-CE4B-4FDE-A460-BA3047D5D6BA',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'I''m Happy Just to Dance With You',
    'A4',
    '1:56',
    'Lead Vocals - Harrison'
    )

	-- The Beatles, And I Love Her
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '99D0CE15-589D-451F-9BAD-B09F482F1508',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'And I Love Her',
    'A5',
    '2:30',
    'Lead Vocals - McCartney'
    )

	-- The Beatles, Tell Me Why
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    'BABBE5AF-4280-4E7B-BF62-56AF8C50A3EB',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'Tell Me Why',
    'A6',
    '2:09',
    'Lead Vocals - Lennon'
    )

	-- The Beatles, Can't Buy Me Love
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    'D8F38F28-20EC-4092-B590-7F8897156653',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'Can''t Buy Me Love',
    'A7',
    '2:12',
    'Lead Vocals - McCartney'
    )

	-- The Beatles, Any Time at All
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '15EC5D50-D95E-451B-802D-E8A6E0F731B0',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'Any Time at All',
    'B1',
    '2:11',
    'Lead Vocals - Lennon'
    )

	-- The Beatles, I'll Cry Instead
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '923C0D6D-040B-49B2-AA45-8FEC5A63123B',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'I''ll Cry Instead',
    'B2',
    '1:44',
    'Lead Vocals - Lennon'
    )

	-- The Beatles, Things We Said Today
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    'B6F778B4-0250-4112-8E8D-47079716154F',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'Things We Said Today',
    'B3',
    '2:35',
    'Lead Vocals - McCartney'
    )

	-- The Beatles, When I Get Home
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '9E7B230C-178C-4A91-8ADF-9F09D1F4B92F',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'When I Get Home',
    'B4',
    '2:17',
    'Lead Vocals - Lennon'
    )

	-- The Beatles, You Can't Do That
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '3AA9A942-27B6-489C-9048-C42B63417CDA',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'You Can''t Do That',
    'B5',
    '2:35',
    'Lead Vocals - Lennon'
    )

	-- The Beatles, I'll Be Back
    INSERT INTO Song (SongId, AlbumId, Title, TrackNumber, Duration, Credits)
    VALUES (
    '5DFC5B8C-6703-441D-945D-5EF9AE8F3C2E',
    'D72003B8-F617-4C19-B163-AA748BEC8297',
    'I''ll Be Back',
    'B6',
    '2:24',
    'Lead Vocals - Lennon with McCartney'
    )

    PRINT '13 SongWriter entries'
    -- The Beatles, A Hard Day's Night
	-- -------------------------------
	-- A Hard Days Night, John Lennon and Paul McCartney
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'B745ED6F-E5DC-4860-9B4B-8EFAC7039B48',  -- SongWriterId
    '1AA4333F-A00C-4EC8-A0B3-50E9E0C11FA4',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '48DE7FB7-EF77-4844-8DF4-CEED2D555C9C',  -- SongWriterId
    '1AA4333F-A00C-4EC8-A0B3-50E9E0C11FA4',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- I Should Have Known Better, John Lennon and Paul McCartney
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '428E9E22-8AB8-4146-A8C3-2F6DB9B56C98',  -- SongWriterId
    'C2EED7A8-1C81-4026-9116-549841979A69',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'BF6D4363-9293-40B6-A42F-9E3889E93D95',  -- SongWriterId
    'C2EED7A8-1C81-4026-9116-549841979A69',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- If I Fell, John Lennon and Paul McCartney
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '2959C3E3-A4DD-4F7C-A935-50F41E08E7A4',  -- SongWriterId
    '6BC56A5C-E3EA-4AC0-90C3-D0B23BB1DD42',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '449FE561-80D7-4BFC-9266-C10A151E02D3',  -- SongWriterId
    '6BC56A5C-E3EA-4AC0-90C3-D0B23BB1DD42',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- I'm Happy Just to Dance With You
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '69C45CB7-FAB2-4297-A63A-03B9DD77BADF',  -- SongWriterId
    'DEA36910-CE4B-4FDE-A460-BA3047D5D6BA',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '79061444-5445-4A2E-BE71-21DDD74C5C5E',  -- SongWriterId
    'DEA36910-CE4B-4FDE-A460-BA3047D5D6BA',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- And I Love Her
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '8780C5EC-D761-4C5E-BC4D-619C61F2EE03',  -- SongWriterId
    '99D0CE15-589D-451F-9BAD-B09F482F1508',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '6FE77168-7BBF-4405-A9E8-F87D27F33FB5',  -- SongWriterId
    '99D0CE15-589D-451F-9BAD-B09F482F1508',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- Tell Me Why
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'FEA88469-6102-4026-8F7E-C2797A228B79',  -- SongWriterId
    'BABBE5AF-4280-4E7B-BF62-56AF8C50A3EB',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '482C9383-A374-46E3-AC24-9C7C87555A46',  -- SongWriterId
    'BABBE5AF-4280-4E7B-BF62-56AF8C50A3EB',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- Can't Buy Me Love
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '289C4724-DF60-4D75-A5BF-74206F32C4BC',  -- SongWriterId
    'D8F38F28-20EC-4092-B590-7F8897156653',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '434B57EA-1D77-4EEC-95A6-FA10FCD237F7',  -- SongWriterId
    'D8F38F28-20EC-4092-B590-7F8897156653',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- Any Time at All
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '38020DBC-3972-4EBB-AE19-D6DFF5E18FD6',  -- SongWriterId
    '15EC5D50-D95E-451B-802D-E8A6E0F731B0',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'DBC227D5-6180-425D-BF34-03D11B4ED50E',  -- SongWriterId
    '15EC5D50-D95E-451B-802D-E8A6E0F731B0',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- I'll Cry Instead
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '31BA92A1-5707-4941-9263-66EC58842AA9',  -- SongWriterId
    '923C0D6D-040B-49B2-AA45-8FEC5A63123B',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'EC4CE26F-42AE-49ED-A1BD-985ABFC8D69B',  -- SongWriterId
    '923C0D6D-040B-49B2-AA45-8FEC5A63123B',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- Things We Said Today
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'FFE4B0CF-E799-424F-B26F-A18CA62F3998',  -- SongWriterId
    'B6F778B4-0250-4112-8E8D-47079716154F',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'BCCF5B74-395A-4FCC-8B87-425EC2D9E9FD',  -- SongWriterId
    'B6F778B4-0250-4112-8E8D-47079716154F',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- When I Get Home
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '9260C05A-EDC5-4469-BC87-5D3F2A3794E6',  -- SongWriterId
    '9E7B230C-178C-4A91-8ADF-9F09D1F4B92F',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'F2CF74EB-9A70-4657-972B-B07777F7B369',  -- SongWriterId
    '9E7B230C-178C-4A91-8ADF-9F09D1F4B92F',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- You Can't Do That
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'EC2B97A6-8F1E-42CD-9D8A-B1125A6F98BB',  -- SongWriterId
    '3AA9A942-27B6-489C-9048-C42B63417CDA',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'D8F33B7C-6089-42CC-86BE-61F99C845BE0',  -- SongWriterId
    '3AA9A942-27B6-489C-9048-C42B63417CDA',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )

    -- I'll Be Back
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    'F106DBDB-D5B4-4740-A331-2FE383D4DA36',  -- SongWriterId
    '5DFC5B8C-6703-441D-945D-5EF9AE8F3C2E',  -- SongId
    '49C233C2-AF63-4682-AA6B-E3280432F123'   -- ArtistId   John Lennon
    )
    INSERT INTO SongWriter (SongWriterId, SongId, ArtistId)
    VALUES (
    '26958159-3A8F-4F52-9756-29B7411C3637',  -- SongWriterId
    '5DFC5B8C-6703-441D-945D-5EF9AE8F3C2E',  -- SongId
    'AAF779A0-D5D4-451C-9910-6C26D937D791'   -- ArtistId   Paul McCartney
    )


    -- Commit transaction
    PRINT 'Commit the transaction'
    COMMIT TRANSACTION;

    PRINT 'MusicCollection seed data was inserted successfully.'
END TRY
BEGIN CATCH
    -- Rollback the transaction because of an error
    PRINT 'Rollback the transaction because of an error.'
    ROLLBACK TRANSACTION;

    -- Display error
    PRINT 'An error occurred while inserting seed data.'
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH;
