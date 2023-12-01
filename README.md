# ToDoApp_Web_Api
Oncelikle layihenin .Net Core 6 da ve N-Tier arxitekturasi ile yazildigini qeyd edim. 
Funksiyanalliq:
Layihemde register, login, istifadecilere baxmaq, silmek, istifadeci melumatlarini deyismek kimi funksiyanalliqlar var. 
Login hissede jwt token ve refresh tokenden istifade etmisem.
Bu sebeble Istifadeciler login olub authorized olduqdan sonra sadece selahiyyetleri olan isleri edebilirler.
Sprint elave etmek, deyismek, silmek;
Assignment elave etmek, deyismek, silmek;
Istifadeciler ve bu istifadecilerin rollari var;
Layihe Istifadeci rollarina gore iki hisseye bolunmusdur. Project manager ve developer.
Project Managerin selahiyetleri: 
Sprintlerin ve assignmentlarin butun proseslerine nezaret edebilir;
Assignmentlere developerleri elave etmek, silmek veya bu developerlerin bir assignmentden basqa bir assignmente yerini deyismek; 
Assignmentin statusunu deyismek;
Butun sprintleri ve o sprintlere aid olan assignmentleri izlemek;
Assignmenti update ederek aid oldugu sprintini deyismek;
Todo Listin Code Review asamasinda rey yazmaq;
Developerlerin selahiyyetleri:
Butun Assignmentleri gormek;
Yalniz oz assignmentlerini gormek;
Assignmentin statusunu deyismek;
Todo Listin Code Review asamasinda rey yazmaq(Sadece assign edildiyi assignmentle bagli rey yazabiler);
Butun bunlardan elave assignmentlerin expiredate-i bitdiyi zaman onlarin statusunu avtamatik database-de failed olaraq deyisir. Bu Scheduling Cron metoduyla yazilmisdir. 


