﻿

/*
DROP TABLE HISTORY  
DROP TABLE VACANCY
DROP TABLE PROFESSION_SKILL

DROP TABLE REFRESHERING
DROP TABLE REFRESHER_COURSE		/*справочник*/
DROP TABLE EMPLOYEE			
DROP TABLE SPECIALITY			/*справочник*/

*/
insert into APPLICANT(FIRST_NAME, SECOND_NAME,FATHER_NAME, LAST_WORK_SPACE, EDUCATION, BIRTHDAY, FAMILY, SOCIAL_STATUS)
	Values ('Петров','Иван','Иванович', 'ОАО СмартСтрой', ' ТА Скрудж', '21/12/1970', '2 детей', '--')

insert into APPLICANT(FIRST_NAME, SECOND_NAME,FATHER_NAME, LAST_WORK_SPACE, EDUCATION, BIRTHDAY, FAMILY, SOCIAL_STATUS)
	Values ('Андрей','Петров','Иванович', 'ОАО РостехСмарт', ' ГГУ имени скарына', '10/10/1982', '--', '--')

/*****************************************************************/

insert into SHERE_PROFESSION(NAME )
	Values ('IT')
insert into SHERE_PROFESSION(NAME )
	Values ('Легкая промышленность')
/*****************************************************************/

insert into PROFESSION(NAME, ID_SHERE_PROFESSION)
	Values ('Программист',1)
insert into PROFESSION(NAME, ID_SHERE_PROFESSION)
	Values ('Системный Администратор',1)
insert into PROFESSION(NAME, ID_SHERE_PROFESSION)
	Values ('Швея',2)
insert into PROFESSION(NAME, ID_SHERE_PROFESSION)
	Values ('Раскройщик',2)

/******************************************************************/
insert into ORGANIZATION(NAME, ABOUT)
	Values ('IBA','Онтсортинговая компания')
insert into ORGANIZATION(NAME, ABOUT)
	Values ('8 Марта','Трикотажная фабрика')
/*****************************************************************/

insert into SPECIALITY(NAME, SYSTEM_SHEMA )
	Values ('Директор','--')
insert into SPECIALITY(NAME, SYSTEM_SHEMA)
	Values ('Сотрудник','--')
/******************************************************************/

insert into EMPLOYEE(FIRST_NAME, SECOND_NAME,FATHER_NAME, BIRTHDAY, ID_SPECIALITY)
	Values ('Шмерцов ','Николай','никифирович', '15/11/1980', 1)
insert into EMPLOYEE(FIRST_NAME, SECOND_NAME,FATHER_NAME, BIRTHDAY, ID_SPECIALITY)
	Values ('Пункол','Никифор','Шкрадж', '8/5/1989', 2)










