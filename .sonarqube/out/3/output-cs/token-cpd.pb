˙
_C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\AddressesController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 
AddressesController

 $
:

% &
ControllerBase

' 5
{ 
private 
readonly 
IRepository $
<$ %
Address% ,
>, -
_addressRepository. @
;@ A
public 
AddressesController "
(" #
IRepository# .
<. /
Address/ 6
>6 7
addressRepository8 I
)I J
{ 	
_addressRepository 
=  
addressRepository! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_addressRepository (
.( )
All) ,
(, -
)- .
). /
;/ 0
} 	
[ 	
HttpGet	 
( 
$str #
)# $
]$ %
public 
IActionResult 
Get  
(  !
Guid! %
	addressId& /
)/ 0
{ 	
return 
Ok 
( 
_addressRepository (
.( )
Get) ,
(, -
	addressId- 6
)6 7
)7 8
;8 9
} 	
[ 	
HttpPost	 
] 
public   
IActionResult   
Create   #
(  # $
[  $ %
FromBody  % -
]  - .
CreateAddressDto  / ?
dto  @ C
)  C D
{!! 	
var"" 
address"" 
="" 
new"" 
Address"" %
(""% &
dto""& )
."") *
Number""* 0
,""0 1
dto""2 5
.""5 6
Street""6 <
,""< =
dto""> A
.""A B
City""B F
,""F G
dto""H K
.""K L
Country""L S
)""S T
;""T U
_addressRepository$$ 
.$$ 
Add$$ "
($$" #
address$$# *
)$$* +
;$$+ ,
_addressRepository%% 
.%% 
SaveChanges%% *
(%%* +
)%%+ ,
;%%, -
return'' 
Created'' 
('' 
nameof'' !
(''! "
Get''" %
)''% &
,''& '
address''( /
)''/ 0
;''0 1
}(( 	
})) 
}** ù
^C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\AirportsController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 
AirportsController

 #
:

$ %
ControllerBase

& 4
{ 
private 
readonly 
IRepository $
<$ %
Airport% ,
>, -
_airportRepository. @
;@ A
private 
readonly 
IRepository $
<$ %
Address% ,
>, -
_addressRepository. @
;@ A
public 
AirportsController !
(! "
IRepository" -
<- .
Airport. 5
>5 6
airportRepository7 H
,H I
IRepositoryJ U
<U V
AddressV ]
>] ^
addressRepository_ p
)p q
{ 	
_airportRepository 
=  
airportRepository! 2
;2 3
_addressRepository 
=  
addressRepository! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_airportRepository (
.( )
All) ,
(, -
)- .
). /
;/ 0
} 	
[ 	
HttpGet	 
( 
$str #
)# $
]$ %
public 
IActionResult 
Get  
(  !
Guid! %
	airportId& /
)/ 0
{ 	
return 
Ok 
( 
_airportRepository (
.( )
Get) ,
(, -
	airportId- 6
)6 7
)7 8
;8 9
} 	
[!! 	
HttpPost!!	 
]!! 
public"" 
IActionResult"" 
Create"" #
(""# $
[""$ %
FromBody""% -
]""- .
CreateAirportDto""/ ?
dto""@ C
)""C D
{## 	
var$$ 
address$$ 
=$$ 
_addressRepository$$ ,
.$$, -
Get$$- 0
($$0 1
dto$$1 4
.$$4 5
	AddressId$$5 >
)$$> ?
;$$? @
var&& 
airport&& 
=&& 
Airport&& !
.&&! "
Create&&" (
(&&( )
dto&&) ,
.&&, -
Name&&- 1
,&&1 2
address&&3 :
,&&: ;
dto&&< ?
.&&? @
City&&@ D
)&&D E
.&&E F
Entity&&F L
;&&L M
_airportRepository(( 
.(( 
Add(( "
(((" #
airport((# *
)((* +
;((+ ,
_airportRepository)) 
.)) 
SaveChanges)) *
())* +
)))+ ,
;)), -
return++ 
Created++ 
(++ 
nameof++ !
(++! "
Get++" %
)++% &
,++& '
airport++( /
)++/ 0
;++0 1
},, 	
}-- 
}.. ≤
^C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\BaggagesController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[ 
ApiController 
] 
public		 

class		 
BaggagesController		 #
:		$ %
ControllerBase		& 4
{

 
private 
readonly 
IRepository $
<$ %
Baggage% ,
>, -
_baggageRepository. @
;@ A
private 
readonly 
IRepository $
<$ %
	Passenger% .
>. / 
_passengerRepository0 D
;D E
public 
BaggagesController !
(! "
IRepository 
< 
Baggage 
>  
baggageRepository! 2
,2 3
IRepository 
< 
	Passenger !
>! "
passengerRepository# 6
) 	
{ 	
_baggageRepository 
=  
baggageRepository! 2
;2 3 
_passengerRepository  
=! "
passengerRepository# 6
;6 7
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_baggageRepository (
.( )
All) ,
(, -
)- .
). /
;/ 0
} 	
[ 	
HttpGet	 
( 
$str #
)# $
]$ %
public 
IActionResult 
Get  
(  !
Guid! %
	baggageId& /
)/ 0
{ 	
return   
Ok   
(   
_baggageRepository   (
.  ( )
Get  ) ,
(  , -
	baggageId  - 6
)  6 7
)  7 8
;  8 9
}!! 	
}"" 
}## ÿ

]C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\CompanyController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[ 
ApiController 
] 
public		 

class		 
CompanyController		 "
:		# $
ControllerBase		% 3
{

 
private 
readonly 
IRepository $
<$ %
Company% ,
>, -
_companyRepository. @
;@ A
public 
CompanyController  
(  !
IRepository! ,
<, -
Company- 4
>4 5
companyRepository6 G
)G H
{ 	
_companyRepository 
=  
companyRepository! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_companyRepository (
.( )
All) ,
(, -
)- .
). /
;/ 0
} 	
} 
} •:
]C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\FlightsController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 
FlightsController

 "
:

# $
ControllerBase

% 3
{ 
private 
readonly 
IRepository $
<$ %
Flight% +
>+ ,
_flightRepository- >
;> ?
private 
readonly 
IRepository $
<$ %
Airport% ,
>, -
_airpotRepository. ?
;? @
public 
FlightsController  
(  !
IRepository 
< 
Flight 
> 
flightRepository  0
,0 1
IRepository 
< 
Airport 
>  
airpotRepository! 1
) 	
{ 	
_flightRepository 
= 
flightRepository  0
;0 1
_airpotRepository 
= 
airpotRepository  0
;0 1
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_flightRepository '
.' (
All( +
(+ ,
), -
)- .
;. /
} 	
[ 	
HttpGet	 
( 
$str "
)" #
]# $
public 
IActionResult 
All  
(  !
Guid! %
flightId& .
). /
{   	
return!! 
Ok!! 
(!! 
_flightRepository!! '
.!!' (
Get!!( +
(!!+ ,
flightId!!, 4
)!!4 5
)!!5 6
;!!6 7
}"" 	
[$$ 	
HttpPost$$	 
]$$ 
public%% 
IActionResult%% 
Create%% #
(%%# $
[%%$ %
FromBody%%% -
]%%- .
CreateFlightDto%%/ >
dto%%? B
)%%B C
{&& 	
var'' 
departureAirport''  
=''! "
_airpotRepository''# 4
.''4 5
Get''5 8
(''8 9
dto''9 <
.''< =
DepartureAirportId''= O
)''O P
;''P Q
var(( 
destinationAirport(( "
=((# $
_airpotRepository((% 6
.((6 7
Get((7 :
(((: ;
dto((; >
.((> ? 
DestinationAirportId((? S
)((S T
;((T U
var)) 
result)) 
=)) 
Flight)) 
.** 
Create** 
(** 
dto++ 
.++ 
DepartureDate++ %
,++% &
dto,, 
.,, 
ArrivalDate,, #
,,,# $
dto-- 
.-- 
PassengerCapacity-- )
,--) *
dto.. 
... !
BaggageWeightCapacity.. -
,..- .
dto// 
.// 
MaxWeightPerBaggage// +
,//+ ,
dto00 
.00 (
MaxBaggageWeightPerPassenger00 4
,004 5
dto11 
.11 
MaxBaggageWidth11 '
,11' (
dto22 
.22 
MaxBaggageHeight22 (
,22( )
dto33 
.33 
MaxBaggageLength33 (
,33( )
departureAirport44 $
,44$ %
destinationAirport55 &
)66 
;66 
if88 
(88 
result88 
.88 
	IsFailure88  
)88  !
{99 
return:: 

BadRequest:: !
(::! "
result::" (
.::( )
Error::) .
)::. /
;::/ 0
};; 
var== 
flight== 
=== 
result== 
.==  
Entity==  &
;==& '
_flightRepository>> 
.>> 
Add>> !
(>>! "
flight>>" (
)>>( )
;>>) *
_flightRepository?? 
.?? 
SaveChanges?? )
(??) *
)??* +
;??+ ,
returnAA 
CreatedAA 
(AA 
nameofAA !
(AA! "
AllAA" %
)AA% &
,AA& '
flightAA( .
)AA. /
;AA/ 0
}BB 	
[DD 	
	HttpPatchDD	 
(DD 
$strDD *
)DD* +
]DD+ ,
publicEE 
IActionResultEE 
UpdateEE #
(EE# $
GuidEE$ (
flightIdEE) 1
,EE1 2
[EE3 4
FromBodyEE4 <
]EE< =
UpdateFlightDtoEE> M
dtoEEN Q
)EEQ R
{FF 	
varGG 
flightGG 
=GG 
_flightRepositoryGG *
.GG* +
GetGG+ .
(GG. /
flightIdGG/ 7
)GG7 8
;GG8 9
varII 
	newFlightII 
=II 
FlightII "
.JJ 
CreateJJ 
(JJ 
dtoKK 
.KK 
DepartureDateKK %
,KK% &
dtoLL 
.LL 
ArrivalDateLL #
,LL# $
flightMM 
.MM 
PassengerCapacityMM ,
,MM, -
flightNN 
.NN !
BaggageWeightCapacityNN 0
,NN0 1
flightOO 
.OO 
MaxWeightPerBaggageOO .
,OO. /
flightPP 
.PP (
MaxBaggageWeightPerPassengerPP 7
,PP7 8
flightQQ 
.QQ 
MaxBaggageWidthQQ *
,QQ* +
flightRR 
.RR 
MaxBaggageHeightRR +
,RR+ ,
flightSS 
.SS 
MaxBaggageLengthSS +
,SS+ ,
flightTT 
.TT 
DepartureAirportTT +
,TT+ ,
flightUU 
.UU 
DestinationAirportUU -
)VV 
.WW 
EntityWW 
;WW 
_flightRepositoryYY 
.YY 
UpdateYY $
(YY$ %
	newFlightYY% .
)YY. /
;YY/ 0
_flightRepositoryZZ 
.ZZ 
SaveChangesZZ )
(ZZ) *
)ZZ* +
;ZZ+ ,
return\\ 
	NoContent\\ 
(\\ 
)\\ 
;\\ 
}]] 	
[__ 	

HttpDelete__	 
(__ 
$str__ %
)__% &
]__& '
public`` 
IActionResult`` 
Remove`` #
(``# $
Guid``$ (
flightId``) 1
)``1 2
{aa 	
varbb 
flightbb 
=bb 
_flightRepositorybb *
.bb* +
Getbb+ .
(bb. /
flightIdbb/ 7
)bb7 8
;bb8 9
_flightRepositorycc 
.cc 
Deletecc $
(cc$ %
flightcc% +
)cc+ ,
;cc, -
_flightRepositorydd 
.dd 
SaveChangesdd )
(dd) *
)dd* +
;dd+ ,
returnff 
	NoContentff 
(ff 
)ff 
;ff 
}gg 	
}hh 
}ii ±:
`C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\PassengersController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

  
PassengersController

 %
:

& '
ControllerBase

( 6
{ 
private 
readonly 
IRepository $
<$ %
	Passenger% .
>. / 
_passengerRepository0 D
;D E
private 
readonly 
IRepository $
<$ %
Person% +
>+ ,
_personRepository- >
;> ?
private 
readonly 
IRepository $
<$ %
Flight% +
>+ ,
_flightRepository- >
;> ?
private 
readonly 
IRepository $
<$ %
Allergy% ,
>, -
_allergyRepository. @
;@ A
public  
PassengersController #
(# $
IRepository$ /
</ 0
	Passenger0 9
>9 :
passengerRepository; N
,N O
IRepositoryP [
<[ \
Person\ b
>b c
personRepositoryd t
,t u
IRepository 
< 
Flight 
> 
flightRepository  0
,0 1
IRepository2 =
<= >
Allergy> E
>E F
allergyRepositoryG X
)X Y
{ 	 
_passengerRepository  
=! "
passengerRepository# 6
;6 7
_personRepository 
= 
personRepository  0
;0 1
_flightRepository 
= 
flightRepository  0
;0 1
_allergyRepository 
=  
allergyRepository! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
(  
_passengerRepository *
.* +
All+ .
(. /
)/ 0
)0 1
;1 2
} 	
[   	
HttpGet  	 
(   
$str   
)   
]   
public!! 
IActionResult!! 
AllForFlight!! )
(!!) *
[!!* +
	FromQuery!!+ 4
]!!4 5
Guid!!6 :
flightId!!; C
)!!C D
{"" 	
var## 

passengers## 
=##  
_passengerRepository## 1
.##1 2
All##2 5
(##5 6
)##6 7
;##7 8
return%% 
Ok%% 
(%% 

passengers%%  
.%%  !
Where%%! &
(%%& '
p%%' (
=>%%) +
p%%, -
.%%- .
Flight%%. 4
.%%4 5
Id%%5 7
==%%8 :
flightId%%; C
)%%C D
)%%D E
;%%E F
}&& 	
[(( 	
HttpGet((	 
((( 
$str(( %
)((% &
]((& '
public)) 
IActionResult)) 
Get))  
())  !
Guid))! %
passengerId))& 1
)))1 2
{** 	
return++ 
Ok++ 
(++  
_passengerRepository++ *
.++* +
Get+++ .
(++. /
passengerId++/ :
)++: ;
)++; <
;++< =
},, 	
[.. 	
HttpPost..	 
].. 
public// 
IActionResult// 
Create// #
(//# $
[//$ %
FromBody//% -
]//- .
CreatePassengerDto/// A
dto//B E
)//E F
{00 	
var11 
person11 
=11 
_personRepository11 *
.11* +
Get11+ .
(11. /
dto11/ 2
.112 3
PersonId113 ;
)11; <
;11< =
var22 
flight22 
=22 
_flightRepository22 *
.22* +
Get22+ .
(22. /
dto22/ 2
.222 3
FlightId223 ;
)22; <
;22< =
var33 
baggages33 
=33 
dto33 
.33 
BaggageDtos33 *
.33* +
Select33+ 1
(331 2
dto332 5
=>336 8
new339 <
Baggage33= D
(33D E
dto33E H
.33H I
Weight33I O
,33O P
dto33Q T
.33T U
Width33U Z
,33Z [
dto33\ _
.33_ `
Height33` f
,33f g
dto33h k
.33k l
Length33l r
)33r s
)33s t
.44 
ToList44 
(44 
)44 
;44 
var55 
	allergies55 
=55 
dto55 
.55  

AllergyIds55  *
.55* +
Select55+ 1
(551 2
id552 4
=>555 7
_allergyRepository558 J
.55J K
Get55K N
(55N O
id55O Q
)55Q R
)55R S
.55S T
ToList55T Z
(55Z [
)55[ \
;55\ ]
var77 
result77 
=77 
	Passenger77 "
.77" #
Create77# )
(77) *
person77* 0
,770 1
flight772 8
,778 9
dto77: =
.77= >
Weight77> D
,77D E
baggages77F N
,77N O
	allergies77P Y
)77Y Z
;77Z [
if99 
(99 
result99 
.99 
	IsFailure99  
)99  !
{:: 
return;; 

BadRequest;; !
(;;! "
result;;" (
.;;( )
Error;;) .
);;. /
;;;/ 0
}<< 
var>> 
	passenger>> 
=>> 
result>> "
.>>" #
Entity>># )
;>>) * 
_passengerRepository??  
.??  !
Add??! $
(??$ %
	passenger??% .
)??. /
;??/ 0 
_passengerRepository@@  
.@@  !
SaveChanges@@! ,
(@@, -
)@@- .
;@@. /
returnBB 
CreatedBB 
(BB 
nameofBB !
(BB! "
GetBB" %
)BB% &
,BB& '
	passengerBB( 1
)BB1 2
;BB2 3
}CC 	
[EE 	

HttpDeleteEE	 
]EE 
publicFF 
IActionResultFF 
DeleteFF #
(FF# $
GuidFF$ (
passengerIdFF) 4
)FF4 5
{GG 	
varHH 
	passengerHH 
=HH  
_passengerRepositoryHH 0
.HH0 1
GetHH1 4
(HH4 5
passengerIdHH5 @
)HH@ A
;HHA B 
_passengerRepositoryJJ  
.JJ  !
DeleteJJ! '
(JJ' (
	passengerJJ( 1
)JJ1 2
;JJ2 3 
_passengerRepositoryKK  
.KK  !
SaveChangesKK! ,
(KK, -
)KK- .
;KK. /
returnMM 
	NoContentMM 
(MM 
)MM 
;MM 
}NN 	
}OO 
}PP ”)
\C:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Controllers\PeopleController.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Controllers *
{ 
[ 
Route 

(
 
$str  
)  !
]! "
[		 
ApiController		 
]		 
public

 

class

 
PeopleController

 !
:

" #
ControllerBase

$ 2
{ 
private 
readonly 
IRepository $
<$ %
Person% +
>+ ,
_personRepository- >
;> ?
private 
readonly 
IRepository $
<$ %
Address% ,
>, -
_addressRepository. @
;@ A
public 
PeopleController 
(  
IRepository  +
<+ ,
Person, 2
>2 3
personRepository4 D
,D E
IRepositoryF Q
<Q R
AddressR Y
>Y Z
addressRepository[ l
)l m
{ 	
_personRepository 
= 
personRepository  0
;0 1
_addressRepository 
=  
addressRepository! 2
;2 3
} 	
[ 	
HttpGet	 
] 
public 
IActionResult 
All  
(  !
)! "
{ 	
return 
Ok 
( 
_personRepository '
.' (
All( +
(+ ,
), -
)- .
;. /
} 	
[ 	
HttpGet	 
( 
$str "
)" #
]# $
public 
IActionResult 
Get  
(  !
Guid! %
personId& .
). /
{ 	
return 
Ok 
( 
_personRepository '
.' (
Get( +
(+ ,
personId, 4
)4 5
)5 6
;6 7
} 	
[!! 	
HttpPost!!	 
]!! 
public"" 
IActionResult"" 
Create"" #
(""# $
[""$ %
FromBody""% -
]""- .
CreatePersonDto""/ >
dto""? B
)""B C
{## 	
var$$ 
address$$ 
=$$ 
new$$ 
Address$$ %
($$% &
dto$$& )
.$$) *

AddressDto$$* 4
.$$4 5
Number$$5 ;
,$$; <
dto$$= @
.$$@ A

AddressDto$$A K
.$$K L
Street$$L R
,$$R S
dto$$T W
.$$W X

AddressDto$$X b
.$$b c
City$$c g
,$$g h
dto%% 
.%% 

AddressDto%% 
.%% 
Country%% &
)%%& '
;%%' (
var&& 
result&& 
=&& 
Person&& 
.&&  
Create&&  &
(&&& '
dto&&' *
.&&* +
Name&&+ /
,&&/ 0
dto&&1 4
.&&4 5
Surname&&5 <
,&&< =
dto&&> A
.&&A B
DateOfBirth&&B M
,&&M N
dto&&O R
.&&R S
Gender&&S Y
,&&Y Z
address&&[ b
)&&b c
;&&c d
if'' 
('' 
result'' 
.'' 
	IsFailure''  
)''  !
{(( 
return)) 

BadRequest)) !
())! "
result))" (
.))( )
Error))) .
))). /
;))/ 0
}** 
Person,, 
person,, 
=,, 
result,, "
.,," #
Entity,,# )
;,,) *
_addressRepository-- 
.-- 
Add-- "
(--" #
address--# *
)--* +
;--+ ,
_addressRepository.. 
... 
SaveChanges.. *
(..* +
)..+ ,
;.., -
_personRepository// 
.// 
Add// !
(//! "
person//" (
)//( )
;//) *
_personRepository00 
.00 
SaveChanges00 )
(00) *
)00* +
;00+ ,
return22 
Created22 
(22 
nameof22 !
(22! "
Get22" %
)22% &
,22& '
person22( .
)22. /
;22/ 0
}33 	
[55 	

HttpDelete55	 
(55 
$str55 %
)55% &
]55& '
public66 
IActionResult66 
Delete66 #
(66# $
Guid66$ (
personId66) 1
)661 2
{77 	
var88 
person88 
=88 
_personRepository88 *
.88* +
Get88+ .
(88. /
personId88/ 7
)887 8
;888 9
_personRepository:: 
.:: 
Delete:: $
(::$ %
person::% +
)::+ ,
;::, -
_personRepository;; 
.;; 
SaveChanges;; )
(;;) *
);;* +
;;;+ ,
return== 
	NoContent== 
(== 
)== 
;== 
}>> 	
}?? 
}@@ †
OC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CompanyDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Features '
.' (
	Companies( 1
{ 
public 

class 

CompanyDto 
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
CreationDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} Ø
UC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreateAddressDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
CreateAddressDto !
{ 
public 
string 
Number 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Street 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Country 
{ 
get  #
;# $
set% (
;( )
}* +
}		 
}

 ì
UC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreateAirportDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
CreateAirportDto !
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
	AddressId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
} 
}		 Ø
UC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreateBaggageDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
CreateBaggageDto !
{ 
public 
double 
Weight 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double 
Width 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
Height 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double 
Length 
{ 
get "
;" #
set$ '
;' (
}) *
}		 
}

 ·
TC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreateFlightDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
CreateFlightDto  
{ 
public 
DateTime 
DepartureDate %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
DateTime 
ArrivalDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
PassengerCapacity $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
double !
BaggageWeightCapacity +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public		 
double		 
MaxWeightPerBaggage		 )
{		* +
get		, /
;		/ 0
set		1 4
;		4 5
}		6 7
public

 
double

 (
MaxBaggageWeightPerPassenger

 2
{

3 4
get

5 8
;

8 9
set

: =
;

= >
}

? @
public 
double 
MaxBaggageWidth %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
double 
MaxBaggageHeight &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
MaxBaggageLength &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Guid 
DepartureAirportId &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Guid  
DestinationAirportId (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} 
} ø	
WC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreatePassengerDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
CreatePassengerDto #
{ 
public 
Guid 
PersonId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
Guid 
FlightId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double 
Weight 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
CreateBaggageDto $
>$ %
BaggageDtos& 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public		 
List		 
<		 
Guid		 
>		 

AllergyIds		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
}

 
} ò

TC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\CreatePersonDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Features '
.' (
Persons( /
{ 
public 

class 
CreatePersonDto  
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Surname 
{ 
get  #
;# $
set% (
;( )
}* +
public		 
int		 
Age		 
{		 
get		 
;		 
set		 !
;		! "
}		# $
public

 
DateTime

 
DateOfBirth

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
string 
Gender 
{ 
get "
;" #
set$ '
;' (
}) *
public 
CreateAddressDto 

AddressDto  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
} 
} ä
TC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Dtos\UpdateFlightDto.cs
	namespace 	
FlightManagement
 
. 
API 
. 
Dtos #
{ 
public 

class 
UpdateFlightDto  
{ 
public 
DateTime 
DepartureDate %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
DateTime 
ArrivalDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} Ñ0
GC:\Users\leahu\Desktop\FlightManagement\FlightManagement.API\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddControllers 
(  
)  !
.! "
AddNewtonsoftJson" 3
(3 4
options4 ;
=>< >
options 
. 
SerializerSettings 
. !
ReferenceLoopHandling 4
=5 6!
ReferenceLoopHandling7 L
.L M
IgnoreM S
) 
; 
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder 
. 
Services 
. 
AddDbContext 
< 
DatabaseContext -
>- .
(. /
options 
=> 
options 
. 
	UseSqlite  
(  !
builder 
. 
Configuration 
. 
GetConnectionString 1
(1 2
$str2 G
)G H
,H I
b 	
=>
 
b 
. 
MigrationsAssembly !
(! "
typeof" (
(( )
DatabaseContext) 8
)8 9
.9 :
Assembly: B
.B C
FullNameC K
)K L
)L M
)M N
;N O
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Address' .
>. /
,/ 0
AddressRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Administrator' 4
>4 5
,5 6#
AdministratorRepository7 N
>N O
(O P
)P Q
;Q R
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Airport' .
>. /
,/ 0
AirportRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Allergy' .
>. /
,/ 0
AllergyRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Baggage' .
>. /
,/ 0
BaggageRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Company' .
>. /
,/ 0
CompanyRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Flight' -
>- .
,. /
FlightRepository0 @
>@ A
(A B
)B C
;C D
builder   
.   
Services   
.   
	AddScoped   
<   
IRepository   &
<  & '
	Passenger  ' 0
>  0 1
,  1 2
PassengerRepository  3 F
>  F G
(  G H
)  H I
;  I J
builder!! 
.!! 
Services!! 
.!! 
	AddScoped!! 
<!! 
IRepository!! &
<!!& '
Person!!' -
>!!- .
,!!. /
PersonRepository!!0 @
>!!@ A
(!!A B
)!!B C
;!!C D
builder## 
.## 
Services## 
.## 
AddCors## 
(## 
options##  
=>##! #
{$$ 
options%% 
.%% 
	AddPolicy%% 
(%% 
$str%% ,
,%%, -
policy%%. 4
=>%%5 7
{&& 
policy'' 
.'' 
AllowAnyOrigin'' 
('' 
)'' 
.''  
AllowAnyHeader''  .
(''. /
)''/ 0
.(( 
AllowAnyMethod(( 
((( 
)(( 
;(( 
})) 
))) 
;)) 
}** 
)** 
;** 
var,, 
app,, 
=,, 	
builder,,
 
.,, 
Build,, 
(,, 
),, 
;,, 
if// 
(// 
app// 
.// 
Environment// 
.// 
IsDevelopment// !
(//! "
)//" #
)//# $
{00 
app11 
.11 

UseSwagger11 
(11 
)11 
;11 
app22 
.22 
UseSwaggerUI22 
(22 
)22 
;22 
}33 
app55 
.55 
UseHttpsRedirection55 
(55 
)55 
;55 
app77 
.77 
UseCors77 
(77 
$str77 "
)77" #
;77# $
app99 
.99 
UseAuthorization99 
(99 
)99 
;99 
app;; 
.;; 
MapControllers;; 
(;; 
);; 
;;; 
app== 
.== 
Run== 
(== 
)== 	
;==	 
