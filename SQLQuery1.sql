insert into categories(CategoryName) values('Documentary');
insert into categories(CategoryName) values('Photography');
insert into categories(CategoryName) values('Biography');
insert into categories(CategoryName) values('General Art');

insert into films(title,link,yearmade,imagefile,synopsis,resources)
     values ('Rape of Eurpoa','http://www.imdb.com/title/tt0997088/',2011,'europa.jpg','good film','Resource1');
insert into films(title,link,yearmade,imagefile,synopsis,resources)
     values ('Marwencol','http://www.imdb.com/title/tt1391092/',2011,'marwencol.jpg','good film','Resource1');
insert into films(title,link,yearmade,imagefile,synopsis,resources)
     values ('Who the #$&% Is Jackson Pollock?','http://www.imdb.com/title/tt0487092/',2006,'pollock.jpg','good film','Resource1');

insert into filmcategories values(1,1);
insert into filmcategories values(1,4);
insert into filmcategories values(2,1);
insert into filmcategories values(2,2);
insert into filmcategories values(3,1);
insert into filmcategories values(3,4);

insert into members(email,pwd,membername,avatar,admin)
  values('hal@gmail.com',
  'P@ssword01','Hal Johnson','harold.jpg',0);
insert into members(email,pwd,membername,avatar,admin)
  values('wirth@gmail.com',
  'P@ssword01','Bill Wirth','bill.jpg',1);
insert into members(email,pwd,membername,avatar,admin)
  values('jane@gmail.com',
  'P@ssword01','Jane Smith','jane.jpg',0);
insert into members(email,pwd,membername,avatar,admin)
  values('Martin@gmail.com',
  'P@ssword01','Martin Jones','noname.JPG',1);

insert into reviews values(2,1,'10/13/2017',5,
                           'A really good film','A really good film');
insert into reviews values(2,2,'10/12/2017',4,
                           'A really good film','A really good film');
insert into reviews values(2,3,'10/11/2017',3,
                           'A really good film','A really good film');
insert into reviews values(1,2,'10/01/2017',5,
                           'A really good film','A really good film');
