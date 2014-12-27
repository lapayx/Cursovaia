
select  * FROM SPECIALITY

/*
DROP TABLE HISTORY  
DROP TABLE REFRESHERING
DROP TABLE REFRESHER_COURSE		/*справочник*/
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
/********************************************************************/
insert into V_PROFESSION_SKILL (ID_APPLICANT ,ID_PROFESSION )
	Values(1,1)
	insert into V_PROFESSION_SKILL (ID_APPLICANT ,ID_PROFESSION )
	Values(1,3)
insert into V_PROFESSION_SKILL (ID_APPLICANT ,ID_PROFESSION )
	Values(2,3)
/***********************************************************************************************/


insert into VACANCY(ID_ORGANIZATION, ID_PROFESSION,ABOUT, MIN_EXPERIENCE,MAX_EXPERIENCE,DATE_ADD)
	Values (1,1,'Срочно нужен',0,100, SYSDATETIME())
insert into VACANCY(ID_ORGANIZATION, ID_PROFESSION,ABOUT, MIN_EXPERIENCE,MAX_EXPERIENCE,DATE_ADD)
	Values (1,1,'Срочно нужен',3,100, SYSDATETIME())

/*************************************************************************************************/

insert into HISTORY(ID_EMPLOYEE, ID_VACANCY, ID_APPLICANT,STATUS,DATE_ADD)
	Values (1,1,1,0,  SYSDATETIME())



