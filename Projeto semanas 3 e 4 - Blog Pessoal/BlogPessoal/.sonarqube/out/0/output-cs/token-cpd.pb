¥N
xD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Controllers\PostagensController.cs
	namespace 	
BlogPessoal
 
. 
Controllers !
;! "
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
public 
class 
PostagensController  
:! "
ControllerBase# 1
{ 
private 
readonly 
IUnitOfWork  
_uof! %
;% &
public 

PostagensController 
( 
IUnitOfWork *
uof+ .
). /
{ 
_uof 
= 
uof 
; 
} 
[ 
HttpGet 
] 
public   

async   
Task   
<   
ActionResult   "
<  " #
IEnumerable  # .
<  . /
PostagemResponseDTO  / B
>  B C
>  C D
>  D E
Get  F I
(  I J
)  J K
{!! 
var"" 
	postagens"" 
="" 
await"" 
_uof"" "
.""" #
PostagemRepository""# 5
.""5 6
GetAllAsync""6 A
(""A B
)""B C
;""C D
if$$ 

($$ 
	postagens$$ 
is$$ 
null$$ 
||$$  
!$$! "
	postagens$$" +
.$$+ ,
Any$$, /
($$/ 0
)$$0 1
)$$1 2
return%% 
NotFound%% 
(%% 
$str%% ;
)%%; <
;%%< =
var'' 
postResponseDTO'' 
='' 
	postagens'' '
.''' (
ToPostagemDTOList''( 9
(''9 :
)'': ;
;''; <
return)) 
Ok)) 
()) 
postResponseDTO)) !
)))! "
;))" #
}** 
[,, 
HttpGet,, 
(,, 
$str,, 
),, 
],, 
public-- 

async-- 
Task-- 
<-- 
ActionResult-- "
<--" #
IEnumerable--# .
<--. /
PostagemResponseDTO--/ B
>--B C
>--C D
>--D E
	GetFiltro--F O
(--O P
[--P Q
	FromQuery--Q Z
]--Z [$
PostagensFiltroAutorTema--\ t

postFiltro--u 
)	-- Ä
{.. 
var// 
	postagens// 
=// 
await// 
_uof// "
.//" #
PostagemRepository//# 5
.//5 6#
GetFiltroAutorTemaAsync//6 M
(//M N

postFiltro//N X
)//X Y
;//Y Z
if11 

(11 
	postagens11 
is11 
null11 
)11 
return22 
NotFound22 
(22 
$str22 ;
)22; <
;22< =
var44 
metadata44 
=44 
new44 
{55 	
	postagens66 
.66 

TotalCount66  
,66  !
	postagens77 
.77 
PageSize77 
,77 
	postagens88 
.88 
CurrentPage88 !
,88! "
	postagens99 
.99 

TotalPages99  
,99  !
	postagens:: 
.:: 
HasNext:: 
,:: 
	postagens;; 
.;; 
HasPrevious;; !
,;;! "
}<< 	
;<<	 

Response>> 
.>> 
Headers>> 
.>> 
Append>> 
(>>  
$str>>  ,
,>>, -
JsonConvert>>. 9
.>>9 :
SerializeObject>>: I
(>>I J
metadata>>J R
)>>R S
)>>S T
;>>T U
var@@ 
postResponseDto@@ 
=@@ 
	postagens@@ '
.@@' (
ToPostagemDTOList@@( 9
(@@9 :
)@@: ;
;@@; <
returnBB 
OkBB 
(BB 
	postagensBB 
)BB 
;BB 
}CC 
[DD 
	AuthorizeDD 
]DD 
[EE 
HttpPostEE 
]EE 
publicFF 

asyncFF 
TaskFF 
<FF 
ActionResultFF "
<FF" #
PostagemResponseDTOFF# 6
>FF6 7
>FF7 8
PostFF9 =
(FF= >
PostagemRequestDTOFF> P
postRequestDtoFFQ _
)FF_ `
{GG 
ifHH 

(HH 
postRequestDtoHH 
isHH 
nullHH "
)HH" #
returnII 

BadRequestII 
(II 
$strII /
)II/ 0
;II0 1
varKK 
postKK 
=KK 
postRequestDtoKK !
.KK! "
RequestToPostKK" /
(KK/ 0
)KK0 1
;KK1 2
varMM 

postCriadoMM 
=MM 
_uofMM 
.MM 
PostagemRepositoryMM 0
.MM0 1
CreateMM1 7
(MM7 8
postMM8 <
)MM< =
;MM= >
awaitNN 
_uofNN 
.NN 
CommitAsyncNN 
(NN 
)NN  
;NN  !
varPP 
postCompletoPP 
=PP 
awaitPP  
_uofPP! %
.PP% &
PostagemRepositoryPP& 8
.PP8 9
GetAsyncPP9 A
(PPA B
pPPB C
=>PPD F
pPPG H
.PPH I

PostagemIdPPI S
==PPT V

postCriadoPPW a
.PPa b

PostagemIdPPb l
)PPl m
;PPm n
varRR 
postResponseDtoRR 
=RR 
postCompletoRR *
.RR* +
ToPostResponseDTORR+ <
(RR< =
)RR= >
;RR> ?
returnTT 

StatusCodeTT 
(TT 
StatusCodesTT %
.TT% &
Status201CreatedTT& 6
,TT6 7
postResponseDtoTT8 G
)TTG H
;TTH I
}UU 
[WW 
	AuthorizeWW 
]WW 
[XX 
HttpPutXX 
(XX 
$strXX 
)XX 
]XX 
publicYY 

asyncYY 
TaskYY 
<YY 
ActionResultYY "
<YY" #
PostagemResponseDTOYY# 6
>YY6 7
>YY7 8
PutYY9 <
(YY< =
intYY= @
idYYA C
,YYC D
PostagemUpdateDTOYYE V
postUpdateDtoYYW d
)YYd e
{ZZ 
if[[ 

([[ 
id[[ 
!=[[ 
postUpdateDto[[ 
.[[  

PostagemId[[  *
)[[* +
return\\ 

BadRequest\\ 
(\\ 
$str\\ /
)\\/ 0
;\\0 1
var^^ 
post^^ 
=^^ 
postUpdateDto^^  
.^^  !
UpdateToPost^^! -
(^^- .
)^^. /
;^^/ 0
var`` 
postAtualizado`` 
=`` 
_uof`` !
.``! "
PostagemRepository``" 4
.``4 5
Update``5 ;
(``; <
post``< @
)``@ A
;``A B
awaitaa 
_uofaa 
.aa 
CommitAsyncaa 
(aa 
)aa  
;aa  !
varcc 
postCompletocc 
=cc 
awaitcc  
_uofcc! %
.cc% &
PostagemRepositorycc& 8
.cc8 9
GetAsynccc9 A
(ccA B
pccB C
=>ccD F
pccG H
.ccH I

PostagemIdccI S
==ccT V
postAtualizadoccW e
.cce f

PostagemIdccf p
)ccp q
;ccq r
varee 
novoPostResponseDtoee 
=ee  !
postCompletoee" .
.ee. /
ToPostResponseDTOee/ @
(ee@ A
)eeA B
;eeB C
returngg 
Okgg 
(gg 
novoPostResponseDtogg %
)gg% &
;gg& '
}hh 
[jj 
	Authorizejj 
]jj 
[kk 

HttpDeletekk 
(kk 
$strkk 
)kk 
]kk 
publicll 

asyncll 
Taskll 
<ll 
ActionResultll "
<ll" #
PostagemResponseDTOll# 6
>ll6 7
>ll7 8
Deletell9 ?
(ll? @
intll@ C
idllD F
)llF G
{mm 
varnn 
postnn 
=nn 
awaitnn 
_uofnn 
.nn 
PostagemRepositorynn 0
.nn0 1
GetAsyncnn1 9
(nn9 :
pnn: ;
=>nn< >
pnn? @
.nn@ A

PostagemIdnnA K
==nnL N
idnnO Q
)nnQ R
;nnR S
ifoo 

(oo 
postoo 
isoo 
nulloo 
)oo 
returnpp 
NotFoundpp 
(pp 
$strpp 5
)pp5 6
;pp6 7
varss 
postExcluidoss 
=ss 
_uofss 
.ss  
PostagemRepositoryss  2
.ss2 3
Deletess3 9
(ss9 :
postss: >
)ss> ?
;ss? @
awaittt 
_uoftt 
.tt 
CommitAsynctt 
(tt 
)tt  
;tt  !
varvv 
postCompletovv 
=vv 
awaitvv  
_uofvv! %
.vv% &
PostagemRepositoryvv& 8
.vv8 9
GetAsyncvv9 A
(vvA B
pvvB C
=>vvD F
pvvG H
.vvH I

PostagemIdvvI S
==vvT V
postExcluidovvW c
.vvc d

PostagemIdvvd n
)vvn o
;vvo p
varxx 
postResponseDtoxx 
=xx 
postCompletoxx *
.xx* +
ToPostResponseDTOxx+ <
(xx< =
)xx= >
;xx> ?
returnzz 
Okzz 
(zz 
postResponseDtozz !
)zz! "
;zz" #
}{{ 
}|| Ä3
tD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Controllers\TemasController.cs
	namespace 	
BlogPessoal
 
. 
Controllers !
;! "
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
public 
class 
TemasController 
: 
ControllerBase -
{ 
private 
readonly 
IUnitOfWork  
_uof! %
;% &
public 

TemasController 
( 
IUnitOfWork &
uof' *
)* +
{ 
_uof 
= 
uof 
; 
} 
[ 
HttpGet 
] 
public 

async 
Task 
< 
ActionResult "
<" #
IEnumerable# .
<. /
TemaResponseDTO/ >
>> ?
>? @
>@ A
GetB E
(E F
)F G
{ 
var 
temas 
= 
await 
_uof 
. 
TemaRepository -
.- .
GetAllAsync. 9
(9 :
): ;
;; <
if 

( 
temas 
is 
null 
) 
return 
NotFound 
( 
$str 7
)7 8
;8 9
var!! 
temasResponseDto!! 
=!! 
temas!! $
.!!$ %
ToTemaDTOList!!% 2
(!!2 3
)!!3 4
;!!4 5
return## 
Ok## 
(## 
temasResponseDto## "
)##" #
;### $
}$$ 
[&& 
	Authorize&& 
]&& 
['' 
HttpPost'' 
]'' 
public(( 

async(( 
Task(( 
<(( 
ActionResult(( "
<((" #
TemaRequestDTO((# 1
>((1 2
>((2 3
Post((4 8
(((8 9
TemaRequestDTO((9 G
temaRequestDto((H V
)((V W
{)) 
if** 

(**
 
temaRequestDto** 
is** 
null** !
)**! "
return++ 

BadRequest++ 
(++ 
$str++ /
)++/ 0
;++0 1
var-- 
tema-- 
=-- 
temaRequestDto-- !
.--! "
RequestToTema--" /
(--/ 0
)--0 1
;--1 2
var// 

temaCriado// 
=// 
_uof// 
.// 
TemaRepository// ,
.//, -
Create//- 3
(//3 4
tema//4 8
)//8 9
;//9 :
await00 
_uof00 
.00 
CommitAsync00 
(00 
)00  
;00  !
var22 
TemaResponseDTO22 
=22 

temaCriado22 (
.22( )
ToTemaResponseDTO22) :
(22: ;
)22; <
;22< =
return44 

StatusCode44 
(44 
StatusCodes44 %
.44% &
Status201Created44& 6
,446 7
TemaResponseDTO448 G
)44G H
;44H I
}55 
[77 
	Authorize77 
]77 
[88 
HttpPut88 
(88 
$str88 
)88 
]88 
public99 

async99 
Task99 
<99 
ActionResult99 "
<99" #
TemaResponseDTO99# 2
>992 3
>993 4
Put995 8
(998 9
int999 <
id99= ?
,99? @
TemaUpdateDTO99A N
temaUpdateDto99O \
)99\ ]
{:: 
if;; 

(;; 
id;; 
!=;; 
temaUpdateDto;; 
.;;  
TemaId;;  &
);;& '
return<< 

BadRequest<< 
(<< 
$str<< /
)<</ 0
;<<0 1
var>> 
tema>> 
=>> 
temaUpdateDto>>  
.>>  !
UpdateToTema>>! -
(>>- .
)>>. /
;>>/ 0
var@@ 
temaAtualizado@@ 
=@@ 
_uof@@ !
.@@! "
TemaRepository@@" 0
.@@0 1
Update@@1 7
(@@7 8
tema@@8 <
)@@< =
;@@= >
awaitAA 
_uofAA 
.AA 
CommitAsyncAA 
(AA 
)AA  
;AA  !
varCC 
novoTemaResponseDtoCC 
=CC  !
temaAtualizadoCC" 0
.CC0 1
ToTemaResponseDTOCC1 B
(CCB C
)CCC D
;CCD E
returnEE 
OkEE 
(EE 
novoTemaResponseDtoEE %
)EE% &
;EE& '
}FF 
[HH 
	AuthorizeHH 
]HH 
[II 

HttpDeleteII 
(II 
$strII 
)II 
]II 
publicJJ 

asyncJJ 
TaskJJ 
<JJ 
ActionResultJJ "
<JJ" #
TemaResponseDTOJJ# 2
>JJ2 3
>JJ3 4
DeleteJJ5 ;
(JJ; <
intJJ< ?
idJJ@ B
)JJB C
{KK 
varLL 
temaLL 
=LL 
awaitLL 
_uofLL 
.LL 
TemaRepositoryLL ,
.LL, -
GetAsyncLL- 5
(LL5 6
cLL6 7
=>LL7 9
cLL9 :
.LL: ;
TemaIdLL; A
==LLB D
idLLE G
)LLG H
;LLH I
ifNN 

(NN
 
temaNN 
isNN 
nullNN 
)NN 
returnOO 
NotFoundOO 
(OO 
$strOO 1
)OO1 2
;OO2 3
varQQ 
temaExcluidoQQ 
=QQ 
_uofQQ 
.QQ  
TemaRepositoryQQ  .
.QQ. /
DeleteQQ/ 5
(QQ5 6
temaQQ6 :
)QQ: ;
;QQ; <
awaitRR 
_uofRR 
.RR 
CommitAsyncRR 
(RR 
)RR  
;RR  !
varTT 
temaResponseDtoTT 
=TT 
temaExcluidoTT *
.TT* +
ToTemaResponseDTOTT+ <
(TT< =
)TT= >
;TT> ?
returnVV 
OkVV 
(VV 
temaResponseDtoVV !
)VV! "
;VV" #
}WW 
}XX ˚>
wD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Controllers\UsuariosController.cs
	namespace 	
BlogPessoal
 
. 
Controllers !
;! "
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
public 
class 
UsuariosController 
:  !
ControllerBase" 0
{ 
private 
readonly 
ITokenService "
_tokenService# 0
;0 1
private 
readonly 
UserManager  
<  !
Usuario! (
>( )
_userManager* 6
;6 7
private 
readonly 
RoleManager  
<  !
IdentityRole! -
<- .
int. 1
>1 2
>2 3
_roleManager4 @
;@ A
private 
readonly 
IConfiguration #
_configuration$ 2
;2 3
public 

UsuariosController 
( 
ITokenService +
tokenService, 8
,8 9
UserManager: E
<E F
UsuarioF M
>M N
userManagerO Z
,Z [
RoleManager\ g
<g h
IdentityRoleh t
<t u
intu x
>x y
>y z
roleManager	{ Ü
,
Ü á
IConfiguration
à ñ
configuration
ó §
)
§ •
{ 
_tokenService 
= 
tokenService $
;$ %
_userManager 
= 
userManager "
;" #
_roleManager   
=   
roleManager   "
;  " #
_configuration!! 
=!! 
configuration!! &
;!!& '
}"" 
[$$ 
HttpPost$$ 
($$ 
$str$$ 
)$$ 
]$$ 
public%% 

async%% 
Task%% 
<%% 
ActionResult%% "
>%%" #
	Cadastrar%%$ -
(%%- .
[%%. /
FromBody%%/ 7
]%%7 8
UsuarioRequestDTO%%9 J
userCadastro%%K W
)%%W X
{&& 
var'' 

userExists'' 
='' 
await'' 
_userManager'' +
.''+ ,
FindByEmailAsync'', <
(''< =
userCadastro''= I
.''I J
Email''J O
!''O P
)''P Q
;''Q R
if)) 

()) 

userExists)) 
!=)) 
null)) 
))) 
{** 	
return++ 

StatusCode++ 
(++ 
StatusCodes++ )
.++) *(
Status500InternalServerError++* F
,++F G
new++H K
Response++L T
{,, 
Status-- 
=-- 
$str-- 
,--  
Message.. 
=.. 
$str.. .
}// 
)// 
;// 
}00 	
Usuario11 
user11 
=11 
new11 
(11 
)11 
{22 	
Email33 
=33 
userCadastro33  
.33  !
Email33! &
,33& '
SecurityStamp44 
=44 
Guid44  
.44  !
NewGuid44! (
(44( )
)44) *
.44* +
ToString44+ 3
(443 4
)444 5
,445 6
UserName55 
=55 
userCadastro55 #
.55# $
Username55$ ,
}66 	
;66	 

var88 
result88 
=88 
await88 
_userManager88 '
.88' (
CreateAsync88( 3
(883 4
user884 8
,888 9
userCadastro88: F
.88F G
Senha88G L
)88L M
;88M N
if:: 

(:: 
!:: 
result:: 
.:: 
	Succeeded:: 
):: 
{;; 	
return<< 

StatusCode<< 
(<< 
StatusCodes<< )
.<<) *(
Status500InternalServerError<<* F
,<<F G
new<<H K
Response<<L T
{== 
Status>> 
=>> 
$str>> 
,>>  
Message?? 
=?? 
$str?? /
}@@ 
)@@ 
;@@ 
}AA 	
returnCC 
OkCC 
(CC 
newCC 
ResponseCC 
{CC 
StatusCC  &
=CC' (
$strCC) 2
,CC2 3
MessageCC4 ;
=CC< =
$strCC> _
}CC_ `
)CC` a
;CCa b
}DD 
[FF 
HttpPostFF 
(FF 
$strFF 
)FF 
]FF 
publicGG 

asyncGG 
TaskGG 
<GG 
ActionResultGG "
>GG" #
LoginGG$ )
(GG) *
[GG* +
FromBodyGG+ 3
]GG3 4
UsuarioLoginGG5 A
	userLoginGGB K
)GGK L
{HH 
varJJ 
userJJ 
=JJ 
awaitJJ 
_userManagerJJ %
.JJ% &
FindByEmailAsyncJJ& 6
(JJ6 7
	userLoginJJ7 @
.JJ@ A
EmailJJA F
!JJF G
)JJG H
;JJH I
ifMM 

(MM 
userMM 
isMM 
notMM 
nullMM 
&&MM 
awaitMM  %
_userManagerMM& 2
.MM2 3
CheckPasswordAsyncMM3 E
(MME F
userMMF J
,MMJ K
	userLoginMML U
.MMU V
SenhaMMV [
)MM[ \
)MM\ ]
{NN 	
varPP 
	userRolesPP 
=PP 
awaitPP !
_userManagerPP" .
.PP. /
GetRolesAsyncPP/ <
(PP< =
userPP= A
)PPA B
;PPB C
varRR 

authClainsRR 
=RR 
newRR  
ListRR! %
<RR% &
ClaimRR& +
>RR+ ,
{SS 
newUU 
ClaimUU 
(UU 

ClaimTypesUU $
.UU$ %
EmailUU% *
,UU* +
userUU, 0
.UU0 1
EmailUU1 6
!UU6 7
)UU7 8
,UU8 9
newVV 
ClaimVV 
(VV #
JwtRegisteredClaimNamesVV 1
.VV1 2
JtiVV2 5
,VV5 6
GuidVV7 ;
.VV; <
NewGuidVV< C
(VVC D
)VVD E
.VVE F
ToStringVVF N
(VVN O
)VVO P
)VVP Q
}WW 
;WW 
foreachYY 
(YY 
varYY 
userRoleYY  
inYY! #
	userRolesYY$ -
)YY- .
{ZZ 

authClains\\ 
.\\ 
Add\\ 
(\\ 
new\\ "
Claim\\# (
(\\( )

ClaimTypes\\) 3
.\\3 4
Role\\4 8
,\\8 9
userRole\\: B
)\\B C
)\\C D
;\\D E
}]] 
var__ 
token__ 
=__ 
_tokenService__ %
.__% &
GenerateAccessToken__& 9
(__9 :

authClains__: D
,__D E
_configuration__F T
)__T U
;__U V
awaitaa 
_userManageraa 
.aa 
UpdateAsyncaa *
(aa* +
useraa+ /
)aa/ 0
;aa0 1
returncc 
Okcc 
(cc 
newcc 
{dd 
Tokenee 
=ee 
newee #
JwtSecurityTokenHandleree 3
(ee3 4
)ee4 5
.ee5 6

WriteTokenee6 @
(ee@ A
tokeneeA F
)eeF G
,eeG H

Expirationff 
=ff 
tokenff "
.ff" #
ValidToff# *
}gg 
)gg 
;gg 
}hh 	
returnii 
Unauthorizedii 
(ii 
)ii 
;ii 
}jj 
}yy ﬂ	
kD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Data\BlogDbContext.cs
	namespace 	
BlogPessoal
 
. 
Data 
; 
public 
class 
BlogDbContext 
: 
IdentityDbContext -
<- .
Usuario. 5
,5 6
IdentityRole7 C
<C D
intD G
>G H
,H I
intJ M
>M N
{		 
public 

BlogDbContext 
( 
DbContextOptions )
<) *
BlogDbContext* 7
>7 8
options9 @
)@ A
:A B
baseC G
(G H
optionsH O
)O P
{ 
} 
public 

DbSet 
< 
Postagem 
> 
? 
	Postagens %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 

DbSet 
< 
Tema 
> 
? 
Temas 
{ 
get  #
;# $
set% (
;( )
}* +
} ¿*
ÉD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Mappings\PostagemDTOMappingExtensions.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Mappings #
;# $
public 
static 
class (
PostagemDTOMappingExtensions 0
{ 
public

 

static

 
Postagem

 
?

 
RequestToPost

 )
(

) *
this

* .
PostagemRequestDTO

/ A
postRequestDto

B P
)

P Q
{ 
if 

( 
postRequestDto 
is 
null "
)" #
return 
null 
; 
return 
new 
Postagem 
{ 	
Titulo 
= 
postRequestDto #
.# $
Titulo$ *
,* +
Texto 
= 
postRequestDto "
." #
Texto# (
,( )
Data 
= 
postRequestDto !
.! "
Data" &
,& '
	UsuarioId 
= 
postRequestDto &
.& '
	UsuarioId' 0
,0 1
TemaId 
= 
postRequestDto #
.# $
TemaId$ *
} 	
;	 

} 
public 

static 
Postagem 
? 
UpdateToPost (
(( )
this) -
PostagemUpdateDTO. ?
postUpdateDto@ M
)M N
{ 
if 

( 
postUpdateDto 
is 
null !
)! "
return 
null 
; 
return 
new 
Postagem 
{ 	

PostagemId   
=   
postUpdateDto   &
.  & '

PostagemId  ' 1
,  1 2
Titulo!! 
=!! 
postUpdateDto!! "
.!!" #
Titulo!!# )
,!!) *
Texto"" 
="" 
postUpdateDto"" !
.""! "
Texto""" '
,""' (
Data## 
=## 
postUpdateDto##  
.##  !
Data##! %
,##% &
	UsuarioId$$ 
=$$ 
postUpdateDto$$ %
.$$% &
	UsuarioId$$& /
,$$/ 0
TemaId%% 
=%% 
postUpdateDto%% "
.%%" #
TemaId%%# )
}&& 	
;&&	 

}'' 
public)) 

static)) 
PostagemResponseDTO)) %
?))% &
ToPostResponseDTO))' 8
())8 9
this))9 =
Postagem))> F
post))G K
)))K L
{** 
if++ 

(++ 
post++ 
is++ 
null++ 
)++ 
return,, 
null,, 
;,, 
return.. 
new.. 
PostagemResponseDTO.. &
{// 	

PostagemId00 
=00 
post00 
.00 

PostagemId00 (
,00( )
Titulo11 
=11 
post11 
.11 
Titulo11  
,11  !
Texto22 
=22 
post22 
.22 
Texto22 
,22 
Data33 
=33 
post33 
.33 
Data33 
,33 
	NomeAutor44 
=44 
post44 
.44 
Usuario44 $
.44$ %
UserName44% -
,44- .
NomeTema55 
=55 
post55 
.55 
Tema55  
.55  !
Nome55! %
}66 	
;66	 

}77 
public:: 

static:: 
IEnumerable:: 
<:: 
PostagemResponseDTO:: 1
>::1 2
ToPostagemDTOList::3 D
(::D E
this::E I
IEnumerable::J U
<::U V
Postagem::V ^
>::^ _
post::` d
)::d e
{;; 
if<< 

(<< 
post<< 
is<< 
null<< 
||<< 
!<< 
post<< !
.<<! "
Any<<" %
(<<% &
)<<& '
)<<' (
return== 
new== 
List== 
<== 
PostagemResponseDTO== /
>==/ 0
(==0 1
)==1 2
;==2 3
return?? 
post?? 
.?? 
Select?? 
(?? 
postagem?? #
=>??$ &
new??' *
PostagemResponseDTO??+ >
{@@ 	

PostagemIdAA 
=AA 
postagemAA !
.AA! "

PostagemIdAA" ,
,AA, -
TituloBB 
=BB 
postagemBB 
.BB 
TituloBB $
,BB$ %
TextoCC 
=CC 
postagemCC 
.CC 
TextoCC "
,CC" #
DataDD 
=DD 
postagemDD 
.DD 
DataDD  
,DD  !
	NomeAutorEE 
=EE 
postagemEE  
.EE  !
UsuarioEE! (
.EE( )
UserNameEE) 1
,EE1 2
NomeTemaFF 
=FF 
postagemFF 
.FF  
TemaFF  $
.FF$ %
NomeFF% )
}GG 	
)GG	 

;GG
 
}HH 
}II ¶
D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Mappings\TemaDTOMappingExtensions.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Mappings #
;# $
public 
static 
class $
TemaDTOMappingExtensions ,
{ 
public

 

static

 
Tema

 
?

 
RequestToTema

 %
(

% &
this

& *
TemaRequestDTO

+ 9
temaDto

: A
)

A B
{ 
if 

( 
temaDto 
is 
null 
) 
return 
null 
; 
return 
new 
Tema 
{ 	
Nome 
= 
temaDto 
. 
Nome 
} 	
;	 

} 
public 

static 
Tema 
? 
UpdateToTema $
($ %
this% )
TemaUpdateDTO* 7
temaUpdateDto8 E
)E F
{ 
if 

( 
temaUpdateDto 
is 
null !
)! "
return 
null 
; 
return 
new 
Tema 
{ 	
TemaId 
= 
temaUpdateDto "
." #
TemaId# )
,) *
Nome 
= 
temaUpdateDto  
.  !
Nome! %
} 	
;	 

} 
public"" 

static"" 
TemaResponseDTO"" !
?""! "
ToTemaResponseDTO""# 4
(""4 5
this""5 9
Tema"": >
tema""? C
)""C D
{## 
if$$ 

($$
 
tema$$ 
is$$ 
null$$ 
)$$ 
return%% 
null%% 
;%% 
return'' 
new'' 
TemaResponseDTO'' "
{(( 	
TemaId)) 
=)) 
tema)) 
.)) 
TemaId))  
,))  !
Nome** 
=** 
tema** 
.** 
Nome** 
,** 
QtdPostagens++ 
=++ 
tema++ 
.++  
Postagem++  (
?++( )
.++) *
Count++* /
(++/ 0
)++0 1
??++2 4
$num++5 6
},, 	
;,,	 

}-- 
public00 

static00 
IEnumerable00 
<00 
TemaResponseDTO00 -
>00- .
ToTemaDTOList00/ <
(00< =
this00= A
IEnumerable00B M
<00M N
Tema00N R
>00R S
temas00T Y
)00Y Z
{11 
if22 

(22
 
temas22 
is22 
null22 
||22 
!22 
temas22 "
.22" #
Any22# &
(22& '
)22' (
)22( )
return33 
new33 
List33 
<33 
TemaResponseDTO33 +
>33+ ,
(33, -
)33- .
;33. /
return55 
temas55 
.55 
Select55 
(55 
tema55  
=>55! #
new55$ '
TemaResponseDTO55( 7
{66 	
TemaId77 
=77 
tema77 
.77 
TemaId77  
,77  !
Nome88 
=88 
tema88 
.88 
Nome88 
,88 
QtdPostagens99 
=99 
tema99 
.99  
Postagem99  (
?99( )
.99) *
Count99* /
(99/ 0
)990 1
??992 4
$num995 6
}:: 	
)::	 

;::
 
}<< 
}== ì
ÇD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Mappings\UsuarioDTOMappingExtensions.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Mappings #
;# $
public 
static 
class '
UsuarioDTOMappingExtensions /
{ 
public		 

static		 
Usuario		 
?		 
RequestToUsuario		 +
(		+ ,
this		, 0
UsuarioRequestDTO		1 B
usuRequestDto		C P
)		P Q
{

 
if 

( 
usuRequestDto 
is 
null !
)! "
return 
null 
; 
return 
new 
Usuario 
{ 	
UserName 
= 
usuRequestDto $
.$ %
Username% -
,- .
Email 
= 
usuRequestDto !
.! "
Email" '
,' (
PasswordHash 
= 
usuRequestDto (
.( )
Senha) .
} 	
;	 

} 
public 

static 
Usuario 
? 
UpdateToUsuario *
(* +
this+ /
UsuarioUpdateDTO0 @
usuUpdateDtoA M
)M N
{ 
if 

( 
usuUpdateDto 
is 
null  
)  !
return 
null 
; 
return 
new 
Usuario 
{ 	
Id 
= 
usuUpdateDto 
. 
Id  
,  !
UserName 
= 
usuUpdateDto #
.# $
Username$ ,
,, -
Email 
= 
usuUpdateDto  
.  !
Email! &
,& '
PasswordHash   
=   
usuUpdateDto   '
.  ' (
Senha  ( -
}!! 	
;!!	 

}"" 
public%% 

static%% 
UsuarioResponseDTO%% $
?%%$ %
ToUsuarioDTO%%& 2
(%%2 3
this%%3 7
Usuario%%8 ?
usuario%%@ G
)%%G H
{&& 
if'' 

('' 
usuario'' 
is'' 
null'' 
)'' 
return(( 
null(( 
;(( 
return** 
new** 
UsuarioResponseDTO** %
{++ 	
Id,, 
=,, 
usuario,, 
.,, 
Id,, 
,,, 
Username-- 
=-- 
usuario-- 
.-- 
UserName-- '
,--' (
Email.. 
=.. 
usuario.. 
... 
Email.. !
,..! "
}// 	
;//	 

}00 
}11 Ÿ
zD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Postagens\PostagemRequestDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
	Postagens $
;$ %
public 
class 
PostagemRequestDTO 
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 4
)4 5
]5 6
[		 
StringLength		 
(		 
$num		 
)		 
]		 
public

 

string

 
?

 
Titulo

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
[ 
Required 
( 
ErrorMessage 
= 
$str 3
)3 4
]4 5
[ 
StringLength 
( 
$num 
, 
MinimumLength $
=% &
$num& '
)' (
]( )
public 

string 
? 
Texto 
{ 
get 
; 
set  #
;# $
}% &
public 

DateTime 
? 
Data 
{ 
get 
;  
set! $
;$ %
}& '
[ 
Required 
( 
ErrorMessage 
= 
$str ;
); <
]< =
public 

int 
	UsuarioId 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
( 
ErrorMessage 
= 
$str 8
)8 9
]9 :
public 

int 
TemaId 
{ 
get 
; 
set  
;  !
}" #
} Ø

{D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Postagens\PostagemResponseDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
	Postagens $
;$ %
public 
class 
PostagemResponseDTO  
{ 
public 

int 

PostagemId 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
Titulo 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
Texto 
{ 
get 
; 
set "
;" #
}$ %
public 

DateTime 
? 
Data 
{ 
get 
;  
set! $
;$ %
}& '
public		 

string		 
?		 
	NomeAutor		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 

string

 
?

 
NomeTema

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
} Ï
yD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Postagens\PostagemUpdateDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
	Postagens $
{ 
public 

class 
PostagemUpdateDTO "
:# $
PostagemRequestDTO% 7
{ 
[ 	
Required	 
] 
public 
int 

PostagemId 
{ 
get  #
;# $
set% (
;( )
}* +
}		 
}

 ò
mD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Status\Response.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Status !
;! "
public 
class 
Response 
{ 
public 

string 
? 
Status 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
? 
Message 
{ 
get  
;  !
set" %
;% &
}' (
} î
rD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Temas\TemaRequestDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
; 
public 
class 
TemaRequestDTO 
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 2
)2 3
]3 4
[ 
StringLength 
( 
$num 
, 
MinimumLength #
=$ %
$num& '
)' (
]( )
public		 

string		 
?		 
Nome		 
{		 
get		 
;		 
set		 "
;		" #
}		$ %
}

 ¨
sD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Temas\TemaResponseDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Temas  
;  !
public 
class 
TemaResponseDTO 
{ 
public 

int 
TemaId 
{ 
get 
; 
set  
;  !
}" #
public 

string 
? 
Nome 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
QtdPostagens 
{ 
get !
;! "
set# &
;& '
}( )
} √
qD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Temas\TemaUpdateDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Temas  
;  !
public 
class 
TemaUpdateDTO 
: 
TemaRequestDTO +
{ 
[ 
Required 
] 
public 

int 
TemaId 
{ 
get 
; 
set  
;  !
}" #
}		 ã
xD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Usuarios\UsuarioRequestDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Usuarios #
;# $
public 
class 
UsuarioRequestDTO 
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 2
)2 3
]3 4
[		 
StringLength		 
(		 
$num		 
)		 
]		 
public

 

string

 
Username

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
[ 
Required 
( 
ErrorMessage 
= 
$str 4
)4 5
]5 6
[ 
StringLength 
( 
$num 
) 
] 
[ 
EmailAddress 
] 
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
( 
ErrorMessage 
= 
$str 3
)3 4
]4 5
[ 
StringLength 
( 
$num 
, 
MinimumLength #
=$ %
$num& '
)' (
]( )
public 

string 
Senha 
{ 
get 
; 
set "
;" #
}$ %
} •
yD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Usuarios\UsuarioResponseDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Usuarios #
;# $
public 
class 
UsuarioResponseDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Username 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
} ö
wD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\DTOs\Usuarios\UsuarioUpdateDTO.cs
	namespace 	
BlogPessoal
 
. 
DTOs 
. 
Usuarios #
;# $
public 
class 
UsuarioUpdateDTO 
: 
UsuarioRequestDTO  1
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
} á
êD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Middlewares\Exceptions\ApiExceptionMiddlewareExtensions.cs
	namespace 	
BlogPessoal
 
. 
Middlewares !
.! "

Exceptions" ,
;, -
public 
static 
class ,
 ApiExceptionMiddlewareExtensions 4
{ 
public 

static 
void %
ConfigureExceptionHandler 0
(0 1
this1 5
IApplicationBuilder6 I
appJ M
)M N
{		 
app

 
.

 
UseExceptionHandler

 
(

  
appError

  (
=>

) +
{ 	
appError 
. 
Run 
( 
async 
context &
=>' )
{ 
context 
. 
Response  
.  !

StatusCode! +
=, -
(. /
int/ 2
)2 3
HttpStatusCode3 A
.A B
InternalServerErrorB U
;U V
context 
. 
Response  
.  !
ContentType! ,
=- .
$str/ A
;A B
var 
contextFeature "
=# $
context% ,
., -
Features- 5
.5 6
Get6 9
<9 :$
IExceptionHandlerFeature: R
>R S
(S T
)T U
;U V
if 
( 
contextFeature !
!=" $
null% )
)) *
{ 
await 
context !
.! "
Response" *
.* +

WriteAsync+ 5
(5 6
new6 9
ErrorDetails: F
(F G
)G H
{ 

StatusCode "
=# $
context% ,
., -
Response- 5
.5 6

StatusCode6 @
,@ A
Message 
=  !
contextFeature" 0
.0 1
Error1 6
.6 7
Message7 >
,> ?
Trace 
= 
contextFeature  .
.. /
Error/ 4
.4 5

StackTrace5 ?
} 
. 
ToString 
( 
)  
)  !
;! "
} 
} 
) 
; 
} 	
)	 

;
 
} 
} ı
|D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Middlewares\Exceptions\ErrorDetails.cs
	namespace 	
BlogPessoal
 
. 
Middlewares !
.! "

Exceptions" ,
;, -
public 
class 
ErrorDetails 
{ 
public 

int 

StatusCode 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
? 
Message 
{ 
get  
;  !
set" %
;% &
}' (
public		 

string		 
?		 
Trace		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

override

 
string

 
ToString

 #
(

# $
)

$ %
{ 
return 
JsonSerializer 
. 
	Serialize '
(' (
this( ,
), -
;- .
} 
} è
D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Middlewares\Filters\ApiExceptionFilter.cs
	namespace 	
BlogPessoal
 
. 
Middlewares !
.! "
Filters" )
;) *
public 
class 
ApiExceptionFilter 
:  !
IExceptionFilter" 2
{ 
private 
readonly 
ILogger 
< 
ApiExceptionFilter /
>/ 0
_logger1 8
;8 9
public		 

ApiExceptionFilter		 
(		 
ILogger		 %
<		% &
ApiExceptionFilter		& 8
>		8 9
logger		: @
)		@ A
{

 
_logger 
= 
logger 
; 
} 
public 

void 
OnException 
( 
ExceptionContext ,
context- 4
)4 5
{ 
_logger 
. 
LogError 
( 
context  
.  !
	Exception! *
,* +
$str, =
)= >
;> ?
context 
. 
Result 
= 
new 
ObjectResult )
() *
$str* W
)W X
{ 	

StatusCode 
= 
StatusCodes $
.$ %(
Status500InternalServerError% A
} 	
;	 

} 
} ß
}D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Middlewares\Filters\ApiLoggingFilter.cs
	namespace 	
BlogPessoal
 
. 
Middlewares !
.! "
Filters" )
;) *
public 
class 
ApiLoggingFilter 
: 
IActionFilter ,
{ 
private 
readonly 
ILogger 
< 
ApiLoggingFilter -
>- .
_logger/ 6
;6 7
public 

ApiLoggingFilter 
( 
ILogger #
<# $
ApiLoggingFilter$ 4
>4 5
logger6 <
)< =
{ 
_logger 
= 
logger 
; 
} 
public 

void 
OnActionExecuted  
(  !!
ActionExecutedContext! 6
context7 >
)> ?
{ 
throw 
new #
NotImplementedException )
() *
)* +
;+ ,
} 
public 

void 
OnActionExecuting !
(! ""
ActionExecutingContext" 8
context9 @
)@ A
{   
throw!! 
new!! #
NotImplementedException!! )
(!!) *
)!!* +
;!!+ ,
}"" 
}## ‡Æ
D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Migrations\20260518233607_BancoInicial.cs
	namespace 	
BlogPessoal
 
. 

Migrations  
{ 
public

 

partial

 
class

 
BancoInicial

 %
:

& '
	Migration

( 1
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterDatabase *
(* +
)+ ,
. 

Annotation 
( 
$str +
,+ ,
$str- 6
)6 7
;7 8
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str #
,# $
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ C
,C D(
MySqlValueGenerationStrategyE a
.a b
IdentityColumnb p
)p q
,q r
Name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 D
,D E
	maxLengthF O
:O P
$numQ T
,T U
nullableV ^
:^ _
true` d
)d e
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
NormalizedName "
=# $
table% *
.* +
Column+ 1
<1 2
string2 8
>8 9
(9 :
type: >
:> ?
$str@ N
,N O
	maxLengthP Y
:Y Z
$num[ ^
,^ _
nullable` h
:h i
truej n
)n o
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
ConcurrencyStamp $
=% &
table' ,
., -
Column- 3
<3 4
string4 :
>: ;
(; <
type< @
:@ A
$strB L
,L M
nullableN V
:V W
trueX \
)\ ]
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
} 
, 
constraints 
: 
table "
=># %
{   
table!! 
.!! 

PrimaryKey!! $
(!!$ %
$str!!% 5
,!!5 6
x!!7 8
=>!!9 ;
x!!< =
.!!= >
Id!!> @
)!!@ A
;!!A B
}"" 
)"" 
.## 

Annotation## 
(## 
$str## +
,##+ ,
$str##- 6
)##6 7
;##7 8
migrationBuilder%% 
.%% 
CreateTable%% (
(%%( )
name&& 
:&& 
$str&& #
,&&# $
columns'' 
:'' 
table'' 
=>'' !
new''" %
{(( 
Id)) 
=)) 
table)) 
.)) 
Column)) %
<))% &
int))& )
>))) *
())* +
type))+ /
:))/ 0
$str))1 6
,))6 7
nullable))8 @
:))@ A
false))B G
)))G H
.** 

Annotation** #
(**# $
$str**$ C
,**C D(
MySqlValueGenerationStrategy**E a
.**a b
IdentityColumn**b p
)**p q
,**q r
Nome++ 
=++ 
table++  
.++  !
Column++! '
<++' (
string++( .
>++. /
(++/ 0
type++0 4
:++4 5
$str++6 C
,++C D
	maxLength++E N
:++N O
$num++P R
,++R S
nullable++T \
:++\ ]
false++^ c
)++c d
.,, 

Annotation,, #
(,,# $
$str,,$ 3
,,,3 4
$str,,5 >
),,> ?
,,,? @
RefreshToken--  
=--! "
table--# (
.--( )
Column--) /
<--/ 0
string--0 6
>--6 7
(--7 8
type--8 <
:--< =
$str--> H
,--H I
nullable--J R
:--R S
true--T X
)--X Y
... 

Annotation.. #
(..# $
$str..$ 3
,..3 4
$str..5 >
)..> ?
,..? @"
RefreshTokenExpiryTime// *
=//+ ,
table//- 2
.//2 3
Column//3 9
<//9 :
DateTime//: B
>//B C
(//C D
type//D H
://H I
$str//J W
,//W X
nullable//Y a
://a b
false//c h
)//h i
,//i j
UserName00 
=00 
table00 $
.00$ %
Column00% +
<00+ ,
string00, 2
>002 3
(003 4
type004 8
:008 9
$str00: H
,00H I
	maxLength00J S
:00S T
$num00U X
,00X Y
nullable00Z b
:00b c
true00d h
)00h i
.11 

Annotation11 #
(11# $
$str11$ 3
,113 4
$str115 >
)11> ?
,11? @
NormalizedUserName22 &
=22' (
table22) .
.22. /
Column22/ 5
<225 6
string226 <
>22< =
(22= >
type22> B
:22B C
$str22D R
,22R S
	maxLength22T ]
:22] ^
$num22_ b
,22b c
nullable22d l
:22l m
true22n r
)22r s
.33 

Annotation33 #
(33# $
$str33$ 3
,333 4
$str335 >
)33> ?
,33? @
Email44 
=44 
table44 !
.44! "
Column44" (
<44( )
string44) /
>44/ 0
(440 1
type441 5
:445 6
$str447 E
,44E F
	maxLength44G P
:44P Q
$num44R U
,44U V
nullable44W _
:44_ `
true44a e
)44e f
.55 

Annotation55 #
(55# $
$str55$ 3
,553 4
$str555 >
)55> ?
,55? @
NormalizedEmail66 #
=66$ %
table66& +
.66+ ,
Column66, 2
<662 3
string663 9
>669 :
(66: ;
type66; ?
:66? @
$str66A O
,66O P
	maxLength66Q Z
:66Z [
$num66\ _
,66_ `
nullable66a i
:66i j
true66k o
)66o p
.77 

Annotation77 #
(77# $
$str77$ 3
,773 4
$str775 >
)77> ?
,77? @
EmailConfirmed88 "
=88# $
table88% *
.88* +
Column88+ 1
<881 2
bool882 6
>886 7
(887 8
type888 <
:88< =
$str88> J
,88J K
nullable88L T
:88T U
false88V [
)88[ \
,88\ ]
PasswordHash99  
=99! "
table99# (
.99( )
Column99) /
<99/ 0
string990 6
>996 7
(997 8
type998 <
:99< =
$str99> H
,99H I
nullable99J R
:99R S
true99T X
)99X Y
.:: 

Annotation:: #
(::# $
$str::$ 3
,::3 4
$str::5 >
)::> ?
,::? @
SecurityStamp;; !
=;;" #
table;;$ )
.;;) *
Column;;* 0
<;;0 1
string;;1 7
>;;7 8
(;;8 9
type;;9 =
:;;= >
$str;;? I
,;;I J
nullable;;K S
:;;S T
true;;U Y
);;Y Z
.<< 

Annotation<< #
(<<# $
$str<<$ 3
,<<3 4
$str<<5 >
)<<> ?
,<<? @
ConcurrencyStamp== $
===% &
table==' ,
.==, -
Column==- 3
<==3 4
string==4 :
>==: ;
(==; <
type==< @
:==@ A
$str==B L
,==L M
nullable==N V
:==V W
true==X \
)==\ ]
.>> 

Annotation>> #
(>># $
$str>>$ 3
,>>3 4
$str>>5 >
)>>> ?
,>>? @
PhoneNumber?? 
=??  !
table??" '
.??' (
Column??( .
<??. /
string??/ 5
>??5 6
(??6 7
type??7 ;
:??; <
$str??= G
,??G H
nullable??I Q
:??Q R
true??S W
)??W X
.@@ 

Annotation@@ #
(@@# $
$str@@$ 3
,@@3 4
$str@@5 >
)@@> ?
,@@? @ 
PhoneNumberConfirmedAA (
=AA) *
tableAA+ 0
.AA0 1
ColumnAA1 7
<AA7 8
boolAA8 <
>AA< =
(AA= >
typeAA> B
:AAB C
$strAAD P
,AAP Q
nullableAAR Z
:AAZ [
falseAA\ a
)AAa b
,AAb c
TwoFactorEnabledBB $
=BB% &
tableBB' ,
.BB, -
ColumnBB- 3
<BB3 4
boolBB4 8
>BB8 9
(BB9 :
typeBB: >
:BB> ?
$strBB@ L
,BBL M
nullableBBN V
:BBV W
falseBBX ]
)BB] ^
,BB^ _

LockoutEndCC 
=CC  
tableCC! &
.CC& '
ColumnCC' -
<CC- .
DateTimeOffsetCC. <
>CC< =
(CC= >
typeCC> B
:CCB C
$strCCD Q
,CCQ R
nullableCCS [
:CC[ \
trueCC] a
)CCa b
,CCb c
LockoutEnabledDD "
=DD# $
tableDD% *
.DD* +
ColumnDD+ 1
<DD1 2
boolDD2 6
>DD6 7
(DD7 8
typeDD8 <
:DD< =
$strDD> J
,DDJ K
nullableDDL T
:DDT U
falseDDV [
)DD[ \
,DD\ ]
AccessFailedCountEE %
=EE& '
tableEE( -
.EE- .
ColumnEE. 4
<EE4 5
intEE5 8
>EE8 9
(EE9 :
typeEE: >
:EE> ?
$strEE@ E
,EEE F
nullableEEG O
:EEO P
falseEEQ V
)EEV W
}FF 
,FF 
constraintsGG 
:GG 
tableGG "
=>GG# %
{HH 
tableII 
.II 

PrimaryKeyII $
(II$ %
$strII% 5
,II5 6
xII7 8
=>II9 ;
xII< =
.II= >
IdII> @
)II@ A
;IIA B
}JJ 
)JJ 
.KK 

AnnotationKK 
(KK 
$strKK +
,KK+ ,
$strKK- 6
)KK6 7
;KK7 8
migrationBuilderMM 
.MM 
CreateTableMM (
(MM( )
nameNN 
:NN 
$strNN 
,NN 
columnsOO 
:OO 
tableOO 
=>OO !
newOO" %
{PP 
TemaIdQQ 
=QQ 
tableQQ "
.QQ" #
ColumnQQ# )
<QQ) *
intQQ* -
>QQ- .
(QQ. /
typeQQ/ 3
:QQ3 4
$strQQ5 :
,QQ: ;
nullableQQ< D
:QQD E
falseQQF K
)QQK L
.RR 

AnnotationRR #
(RR# $
$strRR$ C
,RRC D(
MySqlValueGenerationStrategyRRE a
.RRa b
IdentityColumnRRb p
)RRp q
,RRq r
NomeSS 
=SS 
tableSS  
.SS  !
ColumnSS! '
<SS' (
stringSS( .
>SS. /
(SS/ 0
typeSS0 4
:SS4 5
$strSS6 C
,SSC D
	maxLengthSSE N
:SSN O
$numSSP R
,SSR S
nullableSST \
:SS\ ]
falseSS^ c
)SSc d
.TT 

AnnotationTT #
(TT# $
$strTT$ 3
,TT3 4
$strTT5 >
)TT> ?
}UU 
,UU 
constraintsVV 
:VV 
tableVV "
=>VV# %
{WW 
tableXX 
.XX 

PrimaryKeyXX $
(XX$ %
$strXX% /
,XX/ 0
xXX1 2
=>XX3 5
xXX6 7
.XX7 8
TemaIdXX8 >
)XX> ?
;XX? @
}YY 
)YY 
.ZZ 

AnnotationZZ 
(ZZ 
$strZZ +
,ZZ+ ,
$strZZ- 6
)ZZ6 7
;ZZ7 8
migrationBuilder\\ 
.\\ 
CreateTable\\ (
(\\( )
name]] 
:]] 
$str]] (
,]]( )
columns^^ 
:^^ 
table^^ 
=>^^ !
new^^" %
{__ 
Id`` 
=`` 
table`` 
.`` 
Column`` %
<``% &
int``& )
>``) *
(``* +
type``+ /
:``/ 0
$str``1 6
,``6 7
nullable``8 @
:``@ A
false``B G
)``G H
.aa 

Annotationaa #
(aa# $
$straa$ C
,aaC D(
MySqlValueGenerationStrategyaaE a
.aaa b
IdentityColumnaab p
)aap q
,aaq r
RoleIdbb 
=bb 
tablebb "
.bb" #
Columnbb# )
<bb) *
intbb* -
>bb- .
(bb. /
typebb/ 3
:bb3 4
$strbb5 :
,bb: ;
nullablebb< D
:bbD E
falsebbF K
)bbK L
,bbL M
	ClaimTypecc 
=cc 
tablecc  %
.cc% &
Columncc& ,
<cc, -
stringcc- 3
>cc3 4
(cc4 5
typecc5 9
:cc9 :
$strcc; E
,ccE F
nullableccG O
:ccO P
trueccQ U
)ccU V
.dd 

Annotationdd #
(dd# $
$strdd$ 3
,dd3 4
$strdd5 >
)dd> ?
,dd? @

ClaimValueee 
=ee  
tableee! &
.ee& '
Columnee' -
<ee- .
stringee. 4
>ee4 5
(ee5 6
typeee6 :
:ee: ;
$stree< F
,eeF G
nullableeeH P
:eeP Q
trueeeR V
)eeV W
.ff 

Annotationff #
(ff# $
$strff$ 3
,ff3 4
$strff5 >
)ff> ?
}gg 
,gg 
constraintshh 
:hh 
tablehh "
=>hh# %
{ii 
tablejj 
.jj 

PrimaryKeyjj $
(jj$ %
$strjj% :
,jj: ;
xjj< =
=>jj> @
xjjA B
.jjB C
IdjjC E
)jjE F
;jjF G
tablekk 
.kk 

ForeignKeykk $
(kk$ %
namell 
:ll 
$strll F
,llF G
columnmm 
:mm 
xmm  !
=>mm" $
xmm% &
.mm& '
RoleIdmm' -
,mm- .
principalTablenn &
:nn& '
$strnn( 5
,nn5 6
principalColumnoo '
:oo' (
$stroo) -
,oo- .
onDeletepp  
:pp  !
ReferentialActionpp" 3
.pp3 4
Cascadepp4 ;
)pp; <
;pp< =
}qq 
)qq 
.rr 

Annotationrr 
(rr 
$strrr +
,rr+ ,
$strrr- 6
)rr6 7
;rr7 8
migrationBuildertt 
.tt 
CreateTablett (
(tt( )
nameuu 
:uu 
$struu (
,uu( )
columnsvv 
:vv 
tablevv 
=>vv !
newvv" %
{ww 
Idxx 
=xx 
tablexx 
.xx 
Columnxx %
<xx% &
intxx& )
>xx) *
(xx* +
typexx+ /
:xx/ 0
$strxx1 6
,xx6 7
nullablexx8 @
:xx@ A
falsexxB G
)xxG H
.yy 

Annotationyy #
(yy# $
$stryy$ C
,yyC D(
MySqlValueGenerationStrategyyyE a
.yya b
IdentityColumnyyb p
)yyp q
,yyq r
UserIdzz 
=zz 
tablezz "
.zz" #
Columnzz# )
<zz) *
intzz* -
>zz- .
(zz. /
typezz/ 3
:zz3 4
$strzz5 :
,zz: ;
nullablezz< D
:zzD E
falsezzF K
)zzK L
,zzL M
	ClaimType{{ 
={{ 
table{{  %
.{{% &
Column{{& ,
<{{, -
string{{- 3
>{{3 4
({{4 5
type{{5 9
:{{9 :
$str{{; E
,{{E F
nullable{{G O
:{{O P
true{{Q U
){{U V
.|| 

Annotation|| #
(||# $
$str||$ 3
,||3 4
$str||5 >
)||> ?
,||? @

ClaimValue}} 
=}}  
table}}! &
.}}& '
Column}}' -
<}}- .
string}}. 4
>}}4 5
(}}5 6
type}}6 :
:}}: ;
$str}}< F
,}}F G
nullable}}H P
:}}P Q
true}}R V
)}}V W
.~~ 

Annotation~~ #
(~~# $
$str~~$ 3
,~~3 4
$str~~5 >
)~~> ?
} 
, 
constraints
ÄÄ 
:
ÄÄ 
table
ÄÄ "
=>
ÄÄ# %
{
ÅÅ 
table
ÇÇ 
.
ÇÇ 

PrimaryKey
ÇÇ $
(
ÇÇ$ %
$str
ÇÇ% :
,
ÇÇ: ;
x
ÇÇ< =
=>
ÇÇ> @
x
ÇÇA B
.
ÇÇB C
Id
ÇÇC E
)
ÇÇE F
;
ÇÇF G
table
ÉÉ 
.
ÉÉ 

ForeignKey
ÉÉ $
(
ÉÉ$ %
name
ÑÑ 
:
ÑÑ 
$str
ÑÑ F
,
ÑÑF G
column
ÖÖ 
:
ÖÖ 
x
ÖÖ  !
=>
ÖÖ" $
x
ÖÖ% &
.
ÖÖ& '
UserId
ÖÖ' -
,
ÖÖ- .
principalTable
ÜÜ &
:
ÜÜ& '
$str
ÜÜ( 5
,
ÜÜ5 6
principalColumn
áá '
:
áá' (
$str
áá) -
,
áá- .
onDelete
àà  
:
àà  !
ReferentialAction
àà" 3
.
àà3 4
Cascade
àà4 ;
)
àà; <
;
àà< =
}
ââ 
)
ââ 
.
ää 

Annotation
ää 
(
ää 
$str
ää +
,
ää+ ,
$str
ää- 6
)
ää6 7
;
ää7 8
migrationBuilder
åå 
.
åå 
CreateTable
åå (
(
åå( )
name
çç 
:
çç 
$str
çç (
,
çç( )
columns
éé 
:
éé 
table
éé 
=>
éé !
new
éé" %
{
èè 
LoginProvider
êê !
=
êê" #
table
êê$ )
.
êê) *
Column
êê* 0
<
êê0 1
string
êê1 7
>
êê7 8
(
êê8 9
type
êê9 =
:
êê= >
$str
êê? M
,
êêM N
nullable
êêO W
:
êêW X
false
êêY ^
)
êê^ _
.
ëë 

Annotation
ëë #
(
ëë# $
$str
ëë$ 3
,
ëë3 4
$str
ëë5 >
)
ëë> ?
,
ëë? @
ProviderKey
íí 
=
íí  !
table
íí" '
.
íí' (
Column
íí( .
<
íí. /
string
íí/ 5
>
íí5 6
(
íí6 7
type
íí7 ;
:
íí; <
$str
íí= K
,
ííK L
nullable
ííM U
:
ííU V
false
ííW \
)
íí\ ]
.
ìì 

Annotation
ìì #
(
ìì# $
$str
ìì$ 3
,
ìì3 4
$str
ìì5 >
)
ìì> ?
,
ìì? @!
ProviderDisplayName
îî '
=
îî( )
table
îî* /
.
îî/ 0
Column
îî0 6
<
îî6 7
string
îî7 =
>
îî= >
(
îî> ?
type
îî? C
:
îîC D
$str
îîE O
,
îîO P
nullable
îîQ Y
:
îîY Z
true
îî[ _
)
îî_ `
.
ïï 

Annotation
ïï #
(
ïï# $
$str
ïï$ 3
,
ïï3 4
$str
ïï5 >
)
ïï> ?
,
ïï? @
UserId
ññ 
=
ññ 
table
ññ "
.
ññ" #
Column
ññ# )
<
ññ) *
int
ññ* -
>
ññ- .
(
ññ. /
type
ññ/ 3
:
ññ3 4
$str
ññ5 :
,
ññ: ;
nullable
ññ< D
:
ññD E
false
ññF K
)
ññK L
}
óó 
,
óó 
constraints
òò 
:
òò 
table
òò "
=>
òò# %
{
ôô 
table
öö 
.
öö 

PrimaryKey
öö $
(
öö$ %
$str
öö% :
,
öö: ;
x
öö< =
=>
öö> @
new
ööA D
{
ööE F
x
ööG H
.
ööH I
LoginProvider
ööI V
,
ööV W
x
ööX Y
.
ööY Z
ProviderKey
ööZ e
}
ööf g
)
öög h
;
ööh i
table
õõ 
.
õõ 

ForeignKey
õõ $
(
õõ$ %
name
úú 
:
úú 
$str
úú F
,
úúF G
column
ùù 
:
ùù 
x
ùù  !
=>
ùù" $
x
ùù% &
.
ùù& '
UserId
ùù' -
,
ùù- .
principalTable
ûû &
:
ûû& '
$str
ûû( 5
,
ûû5 6
principalColumn
üü '
:
üü' (
$str
üü) -
,
üü- .
onDelete
††  
:
††  !
ReferentialAction
††" 3
.
††3 4
Cascade
††4 ;
)
††; <
;
††< =
}
°° 
)
°° 
.
¢¢ 

Annotation
¢¢ 
(
¢¢ 
$str
¢¢ +
,
¢¢+ ,
$str
¢¢- 6
)
¢¢6 7
;
¢¢7 8
migrationBuilder
§§ 
.
§§ 
CreateTable
§§ (
(
§§( )
name
•• 
:
•• 
$str
•• '
,
••' (
columns
¶¶ 
:
¶¶ 
table
¶¶ 
=>
¶¶ !
new
¶¶" %
{
ßß 
UserId
®® 
=
®® 
table
®® "
.
®®" #
Column
®®# )
<
®®) *
int
®®* -
>
®®- .
(
®®. /
type
®®/ 3
:
®®3 4
$str
®®5 :
,
®®: ;
nullable
®®< D
:
®®D E
false
®®F K
)
®®K L
,
®®L M
RoleId
©© 
=
©© 
table
©© "
.
©©" #
Column
©©# )
<
©©) *
int
©©* -
>
©©- .
(
©©. /
type
©©/ 3
:
©©3 4
$str
©©5 :
,
©©: ;
nullable
©©< D
:
©©D E
false
©©F K
)
©©K L
}
™™ 
,
™™ 
constraints
´´ 
:
´´ 
table
´´ "
=>
´´# %
{
¨¨ 
table
≠≠ 
.
≠≠ 

PrimaryKey
≠≠ $
(
≠≠$ %
$str
≠≠% 9
,
≠≠9 :
x
≠≠; <
=>
≠≠= ?
new
≠≠@ C
{
≠≠D E
x
≠≠F G
.
≠≠G H
UserId
≠≠H N
,
≠≠N O
x
≠≠P Q
.
≠≠Q R
RoleId
≠≠R X
}
≠≠Y Z
)
≠≠Z [
;
≠≠[ \
table
ÆÆ 
.
ÆÆ 

ForeignKey
ÆÆ $
(
ÆÆ$ %
name
ØØ 
:
ØØ 
$str
ØØ E
,
ØØE F
column
∞∞ 
:
∞∞ 
x
∞∞  !
=>
∞∞" $
x
∞∞% &
.
∞∞& '
RoleId
∞∞' -
,
∞∞- .
principalTable
±± &
:
±±& '
$str
±±( 5
,
±±5 6
principalColumn
≤≤ '
:
≤≤' (
$str
≤≤) -
,
≤≤- .
onDelete
≥≥  
:
≥≥  !
ReferentialAction
≥≥" 3
.
≥≥3 4
Cascade
≥≥4 ;
)
≥≥; <
;
≥≥< =
table
¥¥ 
.
¥¥ 

ForeignKey
¥¥ $
(
¥¥$ %
name
µµ 
:
µµ 
$str
µµ E
,
µµE F
column
∂∂ 
:
∂∂ 
x
∂∂  !
=>
∂∂" $
x
∂∂% &
.
∂∂& '
UserId
∂∂' -
,
∂∂- .
principalTable
∑∑ &
:
∑∑& '
$str
∑∑( 5
,
∑∑5 6
principalColumn
∏∏ '
:
∏∏' (
$str
∏∏) -
,
∏∏- .
onDelete
ππ  
:
ππ  !
ReferentialAction
ππ" 3
.
ππ3 4
Cascade
ππ4 ;
)
ππ; <
;
ππ< =
}
∫∫ 
)
∫∫ 
.
ªª 

Annotation
ªª 
(
ªª 
$str
ªª +
,
ªª+ ,
$str
ªª- 6
)
ªª6 7
;
ªª7 8
migrationBuilder
ΩΩ 
.
ΩΩ 
CreateTable
ΩΩ (
(
ΩΩ( )
name
ææ 
:
ææ 
$str
ææ (
,
ææ( )
columns
øø 
:
øø 
table
øø 
=>
øø !
new
øø" %
{
¿¿ 
UserId
¡¡ 
=
¡¡ 
table
¡¡ "
.
¡¡" #
Column
¡¡# )
<
¡¡) *
int
¡¡* -
>
¡¡- .
(
¡¡. /
type
¡¡/ 3
:
¡¡3 4
$str
¡¡5 :
,
¡¡: ;
nullable
¡¡< D
:
¡¡D E
false
¡¡F K
)
¡¡K L
,
¡¡L M
LoginProvider
¬¬ !
=
¬¬" #
table
¬¬$ )
.
¬¬) *
Column
¬¬* 0
<
¬¬0 1
string
¬¬1 7
>
¬¬7 8
(
¬¬8 9
type
¬¬9 =
:
¬¬= >
$str
¬¬? M
,
¬¬M N
nullable
¬¬O W
:
¬¬W X
false
¬¬Y ^
)
¬¬^ _
.
√√ 

Annotation
√√ #
(
√√# $
$str
√√$ 3
,
√√3 4
$str
√√5 >
)
√√> ?
,
√√? @
Name
ƒƒ 
=
ƒƒ 
table
ƒƒ  
.
ƒƒ  !
Column
ƒƒ! '
<
ƒƒ' (
string
ƒƒ( .
>
ƒƒ. /
(
ƒƒ/ 0
type
ƒƒ0 4
:
ƒƒ4 5
$str
ƒƒ6 D
,
ƒƒD E
nullable
ƒƒF N
:
ƒƒN O
false
ƒƒP U
)
ƒƒU V
.
≈≈ 

Annotation
≈≈ #
(
≈≈# $
$str
≈≈$ 3
,
≈≈3 4
$str
≈≈5 >
)
≈≈> ?
,
≈≈? @
Value
∆∆ 
=
∆∆ 
table
∆∆ !
.
∆∆! "
Column
∆∆" (
<
∆∆( )
string
∆∆) /
>
∆∆/ 0
(
∆∆0 1
type
∆∆1 5
:
∆∆5 6
$str
∆∆7 A
,
∆∆A B
nullable
∆∆C K
:
∆∆K L
true
∆∆M Q
)
∆∆Q R
.
«« 

Annotation
«« #
(
««# $
$str
««$ 3
,
««3 4
$str
««5 >
)
««> ?
}
»» 
,
»» 
constraints
…… 
:
…… 
table
…… "
=>
……# %
{
   
table
ÀÀ 
.
ÀÀ 

PrimaryKey
ÀÀ $
(
ÀÀ$ %
$str
ÀÀ% :
,
ÀÀ: ;
x
ÀÀ< =
=>
ÀÀ> @
new
ÀÀA D
{
ÀÀE F
x
ÀÀG H
.
ÀÀH I
UserId
ÀÀI O
,
ÀÀO P
x
ÀÀQ R
.
ÀÀR S
LoginProvider
ÀÀS `
,
ÀÀ` a
x
ÀÀb c
.
ÀÀc d
Name
ÀÀd h
}
ÀÀi j
)
ÀÀj k
;
ÀÀk l
table
ÃÃ 
.
ÃÃ 

ForeignKey
ÃÃ $
(
ÃÃ$ %
name
ÕÕ 
:
ÕÕ 
$str
ÕÕ F
,
ÕÕF G
column
ŒŒ 
:
ŒŒ 
x
ŒŒ  !
=>
ŒŒ" $
x
ŒŒ% &
.
ŒŒ& '
UserId
ŒŒ' -
,
ŒŒ- .
principalTable
œœ &
:
œœ& '
$str
œœ( 5
,
œœ5 6
principalColumn
–– '
:
––' (
$str
––) -
,
––- .
onDelete
——  
:
——  !
ReferentialAction
——" 3
.
——3 4
Cascade
——4 ;
)
——; <
;
——< =
}
““ 
)
““ 
.
”” 

Annotation
”” 
(
”” 
$str
”” +
,
””+ ,
$str
””- 6
)
””6 7
;
””7 8
migrationBuilder
’’ 
.
’’ 
CreateTable
’’ (
(
’’( )
name
÷÷ 
:
÷÷ 
$str
÷÷ !
,
÷÷! "
columns
◊◊ 
:
◊◊ 
table
◊◊ 
=>
◊◊ !
new
◊◊" %
{
ÿÿ 

PostagemId
ŸŸ 
=
ŸŸ  
table
ŸŸ! &
.
ŸŸ& '
Column
ŸŸ' -
<
ŸŸ- .
int
ŸŸ. 1
>
ŸŸ1 2
(
ŸŸ2 3
type
ŸŸ3 7
:
ŸŸ7 8
$str
ŸŸ9 >
,
ŸŸ> ?
nullable
ŸŸ@ H
:
ŸŸH I
false
ŸŸJ O
)
ŸŸO P
.
⁄⁄ 

Annotation
⁄⁄ #
(
⁄⁄# $
$str
⁄⁄$ C
,
⁄⁄C D*
MySqlValueGenerationStrategy
⁄⁄E a
.
⁄⁄a b
IdentityColumn
⁄⁄b p
)
⁄⁄p q
,
⁄⁄q r
Titulo
€€ 
=
€€ 
table
€€ "
.
€€" #
Column
€€# )
<
€€) *
string
€€* 0
>
€€0 1
(
€€1 2
type
€€2 6
:
€€6 7
$str
€€8 E
,
€€E F
	maxLength
€€G P
:
€€P Q
$num
€€R T
,
€€T U
nullable
€€V ^
:
€€^ _
false
€€` e
)
€€e f
.
‹‹ 

Annotation
‹‹ #
(
‹‹# $
$str
‹‹$ 3
,
‹‹3 4
$str
‹‹5 >
)
‹‹> ?
,
‹‹? @
Texto
›› 
=
›› 
table
›› !
.
››! "
Column
››" (
<
››( )
string
››) /
>
››/ 0
(
››0 1
type
››1 5
:
››5 6
$str
››7 E
,
››E F
	maxLength
››G P
:
››P Q
$num
››R U
,
››U V
nullable
››W _
:
››_ `
false
››a f
)
››f g
.
ﬁﬁ 

Annotation
ﬁﬁ #
(
ﬁﬁ# $
$str
ﬁﬁ$ 3
,
ﬁﬁ3 4
$str
ﬁﬁ5 >
)
ﬁﬁ> ?
,
ﬁﬁ? @
Data
ﬂﬂ 
=
ﬂﬂ 
table
ﬂﬂ  
.
ﬂﬂ  !
Column
ﬂﬂ! '
<
ﬂﬂ' (
DateTime
ﬂﬂ( 0
>
ﬂﬂ0 1
(
ﬂﬂ1 2
type
ﬂﬂ2 6
:
ﬂﬂ6 7
$str
ﬂﬂ8 E
,
ﬂﬂE F
nullable
ﬂﬂG O
:
ﬂﬂO P
false
ﬂﬂQ V
)
ﬂﬂV W
,
ﬂﬂW X
	UsuarioId
‡‡ 
=
‡‡ 
table
‡‡  %
.
‡‡% &
Column
‡‡& ,
<
‡‡, -
int
‡‡- 0
>
‡‡0 1
(
‡‡1 2
type
‡‡2 6
:
‡‡6 7
$str
‡‡8 =
,
‡‡= >
nullable
‡‡? G
:
‡‡G H
false
‡‡I N
)
‡‡N O
,
‡‡O P
TemaId
·· 
=
·· 
table
·· "
.
··" #
Column
··# )
<
··) *
int
··* -
>
··- .
(
··. /
type
··/ 3
:
··3 4
$str
··5 :
,
··: ;
nullable
··< D
:
··D E
false
··F K
)
··K L
}
‚‚ 
,
‚‚ 
constraints
„„ 
:
„„ 
table
„„ "
=>
„„# %
{
‰‰ 
table
ÂÂ 
.
ÂÂ 

PrimaryKey
ÂÂ $
(
ÂÂ$ %
$str
ÂÂ% 3
,
ÂÂ3 4
x
ÂÂ5 6
=>
ÂÂ7 9
x
ÂÂ: ;
.
ÂÂ; <

PostagemId
ÂÂ< F
)
ÂÂF G
;
ÂÂG H
table
ÊÊ 
.
ÊÊ 

ForeignKey
ÊÊ $
(
ÊÊ$ %
name
ÁÁ 
:
ÁÁ 
$str
ÁÁ B
,
ÁÁB C
column
ËË 
:
ËË 
x
ËË  !
=>
ËË" $
x
ËË% &
.
ËË& '
	UsuarioId
ËË' 0
,
ËË0 1
principalTable
ÈÈ &
:
ÈÈ& '
$str
ÈÈ( 5
,
ÈÈ5 6
principalColumn
ÍÍ '
:
ÍÍ' (
$str
ÍÍ) -
,
ÍÍ- .
onDelete
ÎÎ  
:
ÎÎ  !
ReferentialAction
ÎÎ" 3
.
ÎÎ3 4
Cascade
ÎÎ4 ;
)
ÎÎ; <
;
ÎÎ< =
table
ÏÏ 
.
ÏÏ 

ForeignKey
ÏÏ $
(
ÏÏ$ %
name
ÌÌ 
:
ÌÌ 
$str
ÌÌ 9
,
ÌÌ9 :
column
ÓÓ 
:
ÓÓ 
x
ÓÓ  !
=>
ÓÓ" $
x
ÓÓ% &
.
ÓÓ& '
TemaId
ÓÓ' -
,
ÓÓ- .
principalTable
ÔÔ &
:
ÔÔ& '
$str
ÔÔ( /
,
ÔÔ/ 0
principalColumn
 '
:
' (
$str
) 1
,
1 2
onDelete
ÒÒ  
:
ÒÒ  !
ReferentialAction
ÒÒ" 3
.
ÒÒ3 4
Cascade
ÒÒ4 ;
)
ÒÒ; <
;
ÒÒ< =
}
ÚÚ 
)
ÚÚ 
.
ÛÛ 

Annotation
ÛÛ 
(
ÛÛ 
$str
ÛÛ +
,
ÛÛ+ ,
$str
ÛÛ- 6
)
ÛÛ6 7
;
ÛÛ7 8
migrationBuilder
ıı 
.
ıı 
CreateIndex
ıı (
(
ıı( )
name
ˆˆ 
:
ˆˆ 
$str
ˆˆ 2
,
ˆˆ2 3
table
˜˜ 
:
˜˜ 
$str
˜˜ )
,
˜˜) *
column
¯¯ 
:
¯¯ 
$str
¯¯  
)
¯¯  !
;
¯¯! "
migrationBuilder
˙˙ 
.
˙˙ 
CreateIndex
˙˙ (
(
˙˙( )
name
˚˚ 
:
˚˚ 
$str
˚˚ %
,
˚˚% &
table
¸¸ 
:
¸¸ 
$str
¸¸ $
,
¸¸$ %
column
˝˝ 
:
˝˝ 
$str
˝˝ (
,
˝˝( )
unique
˛˛ 
:
˛˛ 
true
˛˛ 
)
˛˛ 
;
˛˛ 
migrationBuilder
ÄÄ 
.
ÄÄ 
CreateIndex
ÄÄ (
(
ÄÄ( )
name
ÅÅ 
:
ÅÅ 
$str
ÅÅ 2
,
ÅÅ2 3
table
ÇÇ 
:
ÇÇ 
$str
ÇÇ )
,
ÇÇ) *
column
ÉÉ 
:
ÉÉ 
$str
ÉÉ  
)
ÉÉ  !
;
ÉÉ! "
migrationBuilder
ÖÖ 
.
ÖÖ 
CreateIndex
ÖÖ (
(
ÖÖ( )
name
ÜÜ 
:
ÜÜ 
$str
ÜÜ 2
,
ÜÜ2 3
table
áá 
:
áá 
$str
áá )
,
áá) *
column
àà 
:
àà 
$str
àà  
)
àà  !
;
àà! "
migrationBuilder
ää 
.
ää 
CreateIndex
ää (
(
ää( )
name
ãã 
:
ãã 
$str
ãã 1
,
ãã1 2
table
åå 
:
åå 
$str
åå (
,
åå( )
column
çç 
:
çç 
$str
çç  
)
çç  !
;
çç! "
migrationBuilder
èè 
.
èè 
CreateIndex
èè (
(
èè( )
name
êê 
:
êê 
$str
êê "
,
êê" #
table
ëë 
:
ëë 
$str
ëë $
,
ëë$ %
column
íí 
:
íí 
$str
íí )
)
íí) *
;
íí* +
migrationBuilder
îî 
.
îî 
CreateIndex
îî (
(
îî( )
name
ïï 
:
ïï 
$str
ïï %
,
ïï% &
table
ññ 
:
ññ 
$str
ññ $
,
ññ$ %
column
óó 
:
óó 
$str
óó ,
,
óó, -
unique
òò 
:
òò 
true
òò 
)
òò 
;
òò 
migrationBuilder
öö 
.
öö 
CreateIndex
öö (
(
öö( )
name
õõ 
:
õõ 
$str
õõ +
,
õõ+ ,
table
úú 
:
úú 
$str
úú "
,
úú" #
column
ùù 
:
ùù 
$str
ùù  
)
ùù  !
;
ùù! "
migrationBuilder
üü 
.
üü 
CreateIndex
üü (
(
üü( )
name
†† 
:
†† 
$str
†† .
,
††. /
table
°° 
:
°° 
$str
°° "
,
°°" #
column
¢¢ 
:
¢¢ 
$str
¢¢ #
)
¢¢# $
;
¢¢$ %
}
££ 	
	protected
¶¶ 
override
¶¶ 
void
¶¶ 
Down
¶¶  $
(
¶¶$ %
MigrationBuilder
¶¶% 5
migrationBuilder
¶¶6 F
)
¶¶F G
{
ßß 	
migrationBuilder
®® 
.
®® 
	DropTable
®® &
(
®®& '
name
©© 
:
©© 
$str
©© (
)
©©( )
;
©©) *
migrationBuilder
´´ 
.
´´ 
	DropTable
´´ &
(
´´& '
name
¨¨ 
:
¨¨ 
$str
¨¨ (
)
¨¨( )
;
¨¨) *
migrationBuilder
ÆÆ 
.
ÆÆ 
	DropTable
ÆÆ &
(
ÆÆ& '
name
ØØ 
:
ØØ 
$str
ØØ (
)
ØØ( )
;
ØØ) *
migrationBuilder
±± 
.
±± 
	DropTable
±± &
(
±±& '
name
≤≤ 
:
≤≤ 
$str
≤≤ '
)
≤≤' (
;
≤≤( )
migrationBuilder
¥¥ 
.
¥¥ 
	DropTable
¥¥ &
(
¥¥& '
name
µµ 
:
µµ 
$str
µµ (
)
µµ( )
;
µµ) *
migrationBuilder
∑∑ 
.
∑∑ 
	DropTable
∑∑ &
(
∑∑& '
name
∏∏ 
:
∏∏ 
$str
∏∏ !
)
∏∏! "
;
∏∏" #
migrationBuilder
∫∫ 
.
∫∫ 
	DropTable
∫∫ &
(
∫∫& '
name
ªª 
:
ªª 
$str
ªª #
)
ªª# $
;
ªª$ %
migrationBuilder
ΩΩ 
.
ΩΩ 
	DropTable
ΩΩ &
(
ΩΩ& '
name
ææ 
:
ææ 
$str
ææ #
)
ææ# $
;
ææ$ %
migrationBuilder
¿¿ 
.
¿¿ 
	DropTable
¿¿ &
(
¿¿& '
name
¡¡ 
:
¡¡ 
$str
¡¡ 
)
¡¡ 
;
¡¡ 
}
¬¬ 	
}
√√ 
}ƒƒ „
ÄD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Migrations\20260519135456_RetirandoNome.cs
	namespace 	
BlogPessoal
 
. 

Migrations  
{ 
public		 

partial		 
class		 
RetirandoNome		 &
:		' (
	Migration		) 2
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str 
, 
table 
: 
$str $
)$ %
;% &
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str $
,$ %
table 
: 
$str $
)$ %
;% &
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str .
,. /
table 
: 
$str $
)$ %
;% &
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	AddColumn &
<& '
string' -
>- .
(. /
name 
: 
$str 
, 
table   
:   
$str   $
,  $ %
type!! 
:!! 
$str!! #
,!!# $
	maxLength"" 
:"" 
$num"" 
,"" 
nullable## 
:## 
false## 
,##  
defaultValue$$ 
:$$ 
$str$$  
)$$  !
.%% 

Annotation%% 
(%% 
$str%% +
,%%+ ,
$str%%- 6
)%%6 7
;%%7 8
migrationBuilder'' 
.'' 
	AddColumn'' &
<''& '
string''' -
>''- .
(''. /
name(( 
:(( 
$str(( $
,(($ %
table)) 
:)) 
$str)) $
,))$ %
type** 
:** 
$str**  
,**  !
nullable++ 
:++ 
true++ 
)++ 
.,, 

Annotation,, 
(,, 
$str,, +
,,,+ ,
$str,,- 6
),,6 7
;,,7 8
migrationBuilder.. 
... 
	AddColumn.. &
<..& '
DateTime..' /
>../ 0
(..0 1
name// 
:// 
$str// .
,//. /
table00 
:00 
$str00 $
,00$ %
type11 
:11 
$str11 #
,11# $
nullable22 
:22 
false22 
,22  
defaultValue33 
:33 
new33 !
DateTime33" *
(33* +
$num33+ ,
,33, -
$num33. /
,33/ 0
$num331 2
,332 3
$num334 5
,335 6
$num337 8
,338 9
$num33: ;
,33; <
$num33= >
,33> ?
DateTimeKind33@ L
.33L M
Unspecified33M X
)33X Y
)33Y Z
;33Z [
}44 	
}55 
}66 ˝
tD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Pagination\PagedList.cs
	namespace 	
BlogPessoal
 
. 
Models 
. 

Pagination '
;' (
public 
class 
	PagedList 
< 
T 
> 
: 
List  
<  !
T! "
>" #
where$ )
T* +
:, -
class. 3
{ 
public 

int 
CurrentPage 
{ 
get  
;  !
set" %
;% &
}' (
public 

int 

TotalPages 
{ 
get 
;  
set! $
;$ %
}& '
public 

int 
PageSize 
{ 
get 
; 
set "
;" #
}$ %
public 

int 

TotalCount 
{ 
get 
;  
set! $
;$ %
}& '
public 

bool 
HasPrevious 
=> 
CurrentPage *
>+ ,
$num- .
;. /
public 

bool 
HasNext 
=> 
CurrentPage &
<' (

TotalPages) 3
;3 4
public 

	PagedList 
( 
List 
< 
T 
> 
itens "
," #
int$ '
count( -
,- .
int/ 2

pageNumber3 =
,= >
int? B
pageSizeC K
)K L
{ 

TotalCount 
= 
count 
; 
PageSize 
= 
pageSize 
; 
CurrentPage 
= 

pageNumber  
;  !

TotalCount 
= 
( 
int 
) 
Math 
. 
Ceiling &
(& '
count' ,
/- .
(/ 0
double0 6
)6 7
pageSize7 ?
)? @
;@ A
AddRange 
( 
itens 
) 
; 
} 
public 

static 
	PagedList 
< 
T 
> 
ToPagedList *
(* +

IQueryable+ 5
<5 6
T6 7
>7 8
source9 ?
,? @
intA D

pageNumberE O
,O P
intQ T
pageSizeU ]
)] ^
{ 
var 
count 
= 
source 
. 
Count  
(  !
)! "
;" #
var 
itens 
= 
source 
. 
Skip 
(  

pageNumber  *
-+ ,
$num- .
). /
./ 0
Take0 4
(4 5
pageSize5 =
)= >
.> ?
ToList? E
(E F
)F G
;G H
return 
new 
	PagedList 
< 
T 
> 
(  
itens  %
,% &
count' ,
,, -

pageNumber. 8
,8 9
pageSize: B
)B C
;C D
}   
}!! Ò
ÉD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Pagination\PostagensFiltroAutorTema.cs
	namespace 	
BlogPessoal
 
. 
Models 
. 

Pagination '
;' (
public 
class $
PostagensFiltroAutorTema %
:& '!
QueryStringParameters( =
{ 
public 

int 
? 
AutorId 
{ 
get 
; 
set  #
;# $
}% &
public 

int 
? 
TemaId 
{ 
get 
; 
set "
;" #
}$ %
} Ì	
ÄD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Pagination\QueryStringParameters.cs
	namespace 	
BlogPessoal
 
. 
Models 
. 

Pagination '
;' (
public 
class !
QueryStringParameters "
{ 
const 	
int
 
maxMaxPageSize 
= 
$num !
;! "
public 

int 

PageNumber 
{ 
get 
;  
set! $
;$ %
}& '
=( )
$num* +
;+ ,
private 
int 
	_pageSize 
= 
maxMaxPageSize *
;* +
public		 

int		 
PageSize		 
{

 
get 
{ 	
return 
	_pageSize 
; 
} 	
set 
{ 	
	_pageSize 
= 
( 
value 
<  
maxMaxPageSize! /
)/ 0
?0 1
value2 7
:8 9
maxMaxPageSize: H
;H I
} 	
} 
} ≈
hD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Postagem.cs
	namespace 	
BlogPessoal
 
. 
Models 
; 
[ 
Table 
( 
$str 
) 
] 
public 
class 
Postagem 
{		 
[

 
Key

 
]

 	
public 

int 

PostagemId 
{ 
get 
;  
set! $
;$ %
}& '
[ 
Required 
] 
[ 
StringLength 
( 
$num 
) 
] 
public 

string 
? 
Titulo 
{ 
get 
;  
set! $
;$ %
}& '
[ 
Required 
] 
[ 
StringLength 
( 
$num 
) 
] 
public 

string 
? 
Texto 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
public 

DateTime 
? 
Data 
{ 
get 
;  
set! $
;$ %
}& '
public 

int 
	UsuarioId 
{ 
get 
; 
set  #
;# $
}% &
public 

int 
TemaId 
{ 
get 
; 
set  
;  !
}" #
[ 

JsonIgnore 
] 
public 

virtual 
Usuario 
? 
Usuario #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 

JsonIgnore 
] 
public 

virtual 
Tema 
? 
Tema 
{ 
get  #
;# $
set% (
;( )
}* +
} ™
dD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Tema.cs
	namespace 	
BlogPessoal
 
. 
Models 
; 
[ 
Table 
( 
$str 
) 
] 
public		 
class		 
Tema		 
{

 
public 

Tema 
( 
) 
{ 
Postagem 
= 
new 

Collection !
<! "
Postagem" *
>* +
(+ ,
), -
;- .
} 
[ 
Key 
] 	
public 

int 
TemaId 
{ 
get 
; 
set  
;  !
}" #
[ 
Required 
] 
[ 
StringLength 
( 
$num 
) 
] 
public 

string 
? 
Nome 
{ 
get 
; 
set "
;" #
}$ %
[ 

JsonIgnore 
] 
public 

ICollection 
< 
Postagem 
>  
?  !
Postagem" *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
} Ã
gD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\Usuario.cs
	namespace 	
BlogPessoal
 
. 
Models 
; 
[		 
Table		 
(		 
$str		 
)		 
]		 
public

 
class

 
Usuario

 
:

 
IdentityUser

 #
<

# $
int

$ '
>

' (
{ 
public 

Usuario 
( 
) 
{ 
Postagem 
= 
new 

Collection !
<! "
Postagem" *
>* +
(+ ,
), -
;- .
} 
public 

ICollection 
< 
Postagem 
>  
?  !
Postagem" *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
} ¨
lD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Models\UsuarioLogin.cs
	namespace 	
BlogPessoal
 
. 
Models 
; 
[ 
	NotMapped 

]
 
public 
class 
UsuarioLogin 
{ 
[		 
Required		 
]		 
[

 
EmailAddress

 
]

 
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
] 
public 

string 
Senha 
{ 
get 
; 
set "
;" #
}$ %
} £L
`D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddControllers 
(  
options  '
=>( *
{+ ,
options- 4
.4 5
Filters5 <
.< =
Add= @
(@ A
typeofA G
(G H
ApiExceptionFilterH Z
)Z [
)[ \
;\ ]
}^ _
)` a
.a b
AddJsonOptionsb p
(p q
optionsq x
=>y {
options	| É
.
É Ñ#
JsonSerializerOptions
Ñ ô
.
ô ö
ReferenceHandler
ö ™
=
´ ¨
ReferenceHandler
≠ Ω
.
Ω æ
IgnoreCycles
æ  
)
  À
;
À Ã
builder 
. 
Services 
. 
AddSwaggerGen 
( 
c  
=>! #
{ 
c 
. 

SwaggerDoc 
( 
$str 
, 
new 
OpenApiInfo &
{' (
Title) .
=/ 0
$str1 >
,> ?
Version@ G
=H I
$strJ N
}O P
)P Q
;Q R
c 
. !
AddSecurityDefinition 
( 
$str $
,$ %
new& )!
OpenApiSecurityScheme* ?
{ 
Description 
= 
$str ;
,; <
In   

=   
ParameterLocation   
.   
Header   %
,  % &
Name!! 
=!! 
$str!! 
,!! 
Type"" 
="" 
SecuritySchemeType"" !
.""! "
ApiKey""" (
}## 
)## 
;## 
c&& 
.&& 
OperationFilter&& 
<&& 
Swashbuckle&& !
.&&! "

AspNetCore&&" ,
.&&, -
Filters&&- 4
.&&4 5/
#SecurityRequirementsOperationFilter&&5 X
>&&X Y
(&&Y Z
)&&Z [
;&&[ \
}'' 
)'' 
;'' 
builder** 
.** 
Services** 
.** 
AddControllers** 
(**  
)**  !
;**! "
builder.. 
... 
Services.. 
... 
AddIdentity.. 
<.. 
Usuario.. $
,..$ %
IdentityRole..& 2
<..2 3
int..3 6
>..6 7
>..7 8
(..8 9
)..9 :
...: ;
AddRoles..; C
<..C D
IdentityRole..D P
<..P Q
int..Q T
>..T U
>..U V
(..V W
)..W X
...X Y$
AddEntityFrameworkStores..Y q
<..q r
BlogDbContext..r 
>	.. Ä
(
..Ä Å
)
..Å Ç
.
..Ç É&
AddDefaultTokenProviders
..É õ
(
..õ ú
)
..ú ù
;
..ù û
string22 
?22 

senhaBanco22 
=22 
Environment22  
.22  !"
GetEnvironmentVariable22! 7
(227 8
$str228 K
)22K L
;22L M
string44 
?44 
mySqlConnection44 
=44 
builder44 !
.44! "
Configuration44" /
.44/ 0
GetConnectionString440 C
(44C D
$str44D W
)44W X
;44X Y
string66 
?66 

connection66 
=66 
$"66 
{66 
mySqlConnection66 '
}66' (
$str66( -
{66- .

senhaBanco66. 8
}668 9
$str669 :
"66: ;
;66; <
builder:: 
.:: 
Services:: 
.:: 
AddDbContext:: 
<:: 
BlogDbContext:: +
>::+ ,
(::, -
options::- 4
=>::5 7
options;; 
.;; 
UseMySql;; 
(;; 

connection;; 
,;;  
ServerVersion;;! .
.;;. /

AutoDetect;;/ 9
(;;9 :

connection;;: D
);;D E
);;E F
);;F G
;;;G H
var== 
	secretKey== 
=== 
builder== 
.== 
Configuration== %
[==% &
$str==& 5
]==5 6
??==7 9
throw==: ?
new==@ C
ArgumentException==D U
(==U V
$str==V n
)==n o
;==o p
builder?? 
.?? 
Services?? 
.?? 
AddAuthentication?? "
(??" #
options??# *
=>??+ -
{@@ 
optionsAA 
.AA %
DefaultAuthenticateSchemeAA %
=AA& '
JwtBearerDefaultsAA( 9
.AA9 : 
AuthenticationSchemeAA: N
;AAN O
optionsBB 
.BB "
DefaultChallengeSchemeBB "
=BB# $
JwtBearerDefaultsBB% 6
.BB6 7 
AuthenticationSchemeBB7 K
;BBK L
}CC 
)CC 
.CC 
AddJwtBearerCC 
(CC 
optionsCC 
=>CC 
{DD 
optionsEE 
.EE 
	SaveTokenEE 
=EE 
trueEE 
;EE 
optionsFF 
.FF  
RequireHttpsMetadataFF  
=FF! "
falseFF# (
;FF( )
optionsGG 
.GG %
TokenValidationParametersGG %
=GG& '
newGG( +%
TokenValidationParametersGG, E
(GGE F
)GGF G
{HH 
ValidateIssuerII 
=II 
trueII 
,II 
ValidateAudienceJJ 
=JJ 
trueJJ 
,JJ  
ValidateLifetimeKK 
=KK 
trueKK 
,KK  $
ValidateIssuerSigningKeyLL  
=LL! "
trueLL# '
,LL' (
	ClockSkewMM 
=MM 
TimeSpanMM 
.MM 
ZeroMM !
,MM! "
ValidAudienceNN 
=NN 
builderNN 
.NN  
ConfigurationNN  -
[NN- .
$strNN. A
]NNA B
,NNB C
ValidIssuerOO 
=OO 
builderOO 
.OO 
ConfigurationOO +
[OO+ ,
$strOO, =
]OO= >
,OO> ?
IssuerSigningKeyPP 
=PP 
newPP  
SymmetricSecurityKeyPP 3
(PP3 4
EncodingPP4 <
.PP< =
UTF8PP= A
.PPA B
GetBytesPPB J
(PPJ K
	secretKeyPPK T
)PPT U
)PPU V
}QQ 
;QQ 
}RR 
)RR 
;RR 
builderXX 
.XX 
ServicesXX 
.XX 
	AddScopedXX 
<XX 
ApiLoggingFilterXX +
>XX+ ,
(XX, -
)XX- .
;XX. /
builder\\ 
.\\ 
Services\\ 
.\\ 
	AddScoped\\ 
<\\ 
IPostagemRepository\\ .
,\\. /
PostagemRepository\\/ A
>\\A B
(\\B C
)\\C D
;\\D E
builder]] 
.]] 
Services]] 
.]] 
	AddScoped]] 
<]] 
ITemaRepository]] *
,]]* +
TemaRepository]]+ 9
>]]9 :
(]]: ;
)]]; <
;]]< =
builder^^ 
.^^ 
Services^^ 
.^^ 
	AddScoped^^ 
(^^ 
typeof^^ !
(^^! "
IRepository^^" -
<^^- .
>^^. /
)^^/ 0
,^^0 1
typeof^^2 8
(^^8 9

Repository^^9 C
<^^C D
>^^D E
)^^E F
)^^F G
;^^G H
builder__ 
.__ 
Services__ 
.__ 
	AddScoped__ 
<__ 
IUnitOfWork__ &
,__& '

UnitOfWork__( 2
>__2 3
(__3 4
)__4 5
;__5 6
builderaa 
.aa 
Servicesaa 
.aa 
	AddScopedaa 
<aa 
ITokenServiceaa (
,aa( )
TokenServiceaa* 6
>aa6 7
(aa7 8
)aa8 9
;aa9 :
vargg 
appgg 
=gg 	
buildergg
 
.gg 
Buildgg 
(gg 
)gg 
;gg 
ifjj 
(jj 
appjj 
.jj 
Environmentjj 
.jj 
IsDevelopmentjj !
(jj! "
)jj" #
)jj# $
{kk 
appmm 
.mm 

UseSwaggermm 
(mm 
)mm 
;mm 
appnn 
.nn %
ConfigureExceptionHandlernn !
(nn! "
)nn" #
;nn# $
appoo 
.oo 
UseSwaggerUIoo 
(oo 
optionsoo 
=>oo 
optionsoo  '
.oo' (
SwaggerEndpointoo( 7
(oo7 8
$stroo8 R
,ooR S
$strooT f
)oof g
)oog h
;ooh i
}rr 
apptt 
.tt 
UseHttpsRedirectiontt 
(tt 
)tt 
;tt 
appvv 
.vv 
UseAuthorizationvv 
(vv 
)vv 
;vv 
appxx 
.xx 
MapControllersxx 
(xx 
)xx 
;xx 
appzz 
.zz 
Runzz 
(zz 
)zz 	
;zz	 
ˆ	
ÉD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\GenericRepository\IRepository.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
GenericRepository# 4
;4 5
public 
	interface 
IRepository 
< 
T 
> 
{ 
T 
Create 
( 
T 
entity 
) 
; 
Task		 
<		 	
IEnumerable			 
<		 
T		 
>		 
>		 
GetAllAsync		 $
(		$ %
)		% &
;		& '
Task

 
<

 	
T

	 

?


 
>

 
GetAsync

 
(

 

Expression

  
<

  !
Func

! %
<

% &
T

& '
,

' (
bool

) -
>

- .
>

. /
	predicate

0 9
)

9 :
;

: ;
T 
Update 
( 
T 
entity 
) 
; 
T 
Delete 
( 
T 
entity 
) 
; 
} ˚
ÇD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\GenericRepository\Repository.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
GenericRepository# 4
;4 5
public 
class 

Repository 
< 
T 
> 
: 
IRepository (
<( )
T) *
>* +
where, 1
T2 3
:4 5
class6 ;
{ 
	protected

 
readonly

 
BlogDbContext

 $
_context

% -
;

- .
public 


Repository 
( 
BlogDbContext #
context$ +
)+ ,
{ 
_context 
= 
context 
; 
} 
public 

T 
Create 
( 
T 
entity 
) 
{ 
_context 
. 
Set 
< 
T 
> 
( 
) 
. 
Add 
( 
entity $
)$ %
;% &
return 
entity 
; 
} 
public 

virtual 
async 
Task 
< 
IEnumerable )
<) *
T* +
>+ ,
>, -
GetAllAsync. 9
(9 :
): ;
{ 
return 
await 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
.& '
AsNoTracking' 3
(3 4
)4 5
.5 6
ToListAsync6 A
(A B
)B C
;C D
} 
public 

virtual 
async 
Task 
< 
T 
>  
GetAsync! )
() *

Expression* 4
<4 5
Func5 9
<9 :
T: ;
,; <
bool= A
>A B
>B C
	predicateD M
)M N
{ 
return 
await 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
.& '
FirstOrDefaultAsync' :
(: ;
	predicate; D
)D E
;E F
}   
public"" 

T"" 
Update"" 
("" 
T"" 
entity"" 
)"" 
{## 
_context$$ 
.$$ 
Entry$$ 
($$ 
entity$$ 
)$$ 
.$$ 
State$$ $
=$$% &
EntityState$$' 2
.$$2 3
Modified$$3 ;
;$$; <
return%% 
entity%% 
;%% 
}&& 
public'' 

T'' 
Delete'' 
('' 
T'' 
entity'' 
)'' 
{(( 
_context)) 
.)) 
Set)) 
<)) 
T)) 
>)) 
()) 
))) 
.)) 
Remove))  
())  !
entity))! '
)))' (
;))( )
return** 
entity** 
;** 
}++ 
}.. Á
ÉD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\Postagens\IPostagemRepository.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
	Postagens# ,
;, -
public		 
	interface		 
IPostagemRepository		 $
:		% &
IRepository		' 2
<		2 3
Postagem		3 ;
>		; <
{

 
Task 
< 
	PagedList 
< 
Postagem 
> 
> #
GetFiltroAutorTemaAsync 4
(4 5$
PostagensFiltroAutorTema5 M 
postagemFiltroParamsN b
)b c
;c d
} ®#
ÇD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\Postagens\PostagemRepository.cs
	namespace		 	
BlogPessoal		
 
.		 
Repositories		 "
.		" #
	Postagens		# ,
;		, -
public 
class 
PostagemRepository 
:  !

Repository" ,
<, -
Postagem- 5
>5 6
,6 7
IPostagemRepository8 K
{ 
public 

PostagemRepository 
( 
BlogDbContext +
context, 3
)3 4
:5 6
base7 ;
(; <
context< C
)C D
{ 
} 
public 

override 
async 
Task 
< 
IEnumerable *
<* +
Postagem+ 3
>3 4
>4 5
GetAllAsync6 A
(A B
)B C
{ 
return 
await 
_context 
. 
	Postagens '
.' (
Include( /
(/ 0
p0 1
=>2 4
p5 6
.6 7
Tema7 ;
); <
.< =
Include= D
(D E
pE F
=>G I
pJ K
.K L
UsuarioL S
)S T
.T U
AsNoTrackingU a
(a b
)b c
.c d
ToListAsyncd o
(o p
)p q
;q r
} 
public 

override 
async 
Task 
< 
Postagem '
>' (
GetAsync) 1
(1 2

Expression2 <
<< =
Func= A
<A B
PostagemB J
,J K
boolL P
>P Q
>Q R
	predicateS \
)\ ]
{ 
return 
await 
_context 
. 
	Postagens '
.' (
Include( /
(/ 0
p0 1
=>2 4
p5 6
.6 7
Tema7 ;
); <
.< =
Include= D
(D E
pE F
=>G I
pJ K
.K L
UsuarioL S
)S T
.T U
FirstOrDefaultAsyncU h
(h i
	predicatei r
)r s
;s t
} 
public 

async 
Task 
< 
	PagedList 
<  
Postagem  (
>( )
>) *#
GetFiltroAutorTemaAsync+ B
(B C$
PostagensFiltroAutorTemaC [
postFiltroParams\ l
)l m
{ 
var 
consulta 
= 
await 
GetAllAsync (
(( )
)) *
;* +
if 

( 
postFiltroParams 
. 
AutorId $
!=% '
null( ,
), -
consulta 
= 
consulta 
.  
Where  %
(% &
p& '
=>( *
p+ ,
., -
	UsuarioId- 6
==7 9
postFiltroParams: J
.J K
AutorIdK R
)R S
;S T
if!! 

(!! 
postFiltroParams!! 
.!! 
TemaId!! #
!=!!$ &
null!!' +
)!!+ ,
consulta"" 
="" 
consulta"" 
.""  
Where""  %
(""% &
p""& '
=>""( *
p""+ ,
."", -
TemaId""- 3
==""4 6
postFiltroParams""7 G
.""G H
TemaId""H N
)""N O
;""O P
var%% 
postOredenado%% 
=%% 
consulta%% $
.%%$ %
OrderBy%%% ,
(%%, -
p%%- .
=>%%/ 1
p%%2 3
.%%3 4
Data%%4 8
)%%8 9
.%%9 :
AsQueryable%%: E
(%%E F
)%%F G
;%%G H
return'' 
	PagedList'' 
<'' 
Postagem'' !
>''! "
.''" #
ToPagedList''# .
(''. /
postOredenado''/ <
,''< =
postFiltroParams''> N
.''N O

PageNumber''O Y
,''Y Z
postFiltroParams''[ k
.''k l
PageSize''l t
)''t u
;''u v
}(( 
}.. ø
{D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\Temas\ITemaRepository.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
Temas# (
;( )
public 
	interface 
ITemaRepository  
:! "
IRepository# .
<. /
Tema/ 3
>3 4
{ 
} Ω
zD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\Temas\TemaRepository.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
Temas# (
;( )
public		 
class		 
TemaRepository		 
:		 

Repository		 (
<		( )
Tema		) -
>		- .
,		. /
ITemaRepository		0 ?
{

 
private 
readonly 
BlogDbContext "
_context# +
;+ ,
public 

TemaRepository 
( 
BlogDbContext '
context( /
)/ 0
:1 2
base3 7
(7 8
context8 ?
)? @
{ 
} 
} ∑
}D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\UnitsOfWork\IUnitOfWork.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
UnitsOfWork# .
;. /
public 
	interface 
IUnitOfWork 
{ 
IPostagemRepository 
PostagemRepository *
{+ ,
get- 0
;0 1
}2 3
ITemaRepository		 
TemaRepository		 "
{		# $
get		% (
;		( )
}		* +
Task

 
CommitAsync

	 
(

 
)

 
;

 
} À
|D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Repositories\UnitsOfWork\UnitOfWork.cs
	namespace 	
BlogPessoal
 
. 
Repositories "
." #
UnitsOfWork# .
;. /
public 
class 

UnitOfWork 
: 
IUnitOfWork %
{ 
public		 

IPostagemRepository		 
?		 
_postagemRepository		  3
;		3 4
public 

ITemaRepository 
? 
_temaRepository +
;+ ,
public 

BlogDbContext 
_context !
;! "
public 


UnitOfWork 
( 
BlogDbContext #
context$ +
)+ ,
{ 
_context 
= 
context 
; 
} 
public 

IPostagemRepository 
PostagemRepository 1
{ 
get 
{ 	
if 
( 
_postagemRepository #
is$ &
null' +
)+ ,
{ 
_postagemRepository #
=$ %
new& )
PostagemRepository* <
(< =
_context= E
)E F
;F G
} 
return 
_postagemRepository &
;& '
} 	
} 
public 

ITemaRepository 
TemaRepository )
{   
get!! 
{"" 	
if$$ 
($$ 
_temaRepository$$ 
is$$  "
null$$# '
)$$' (
{%% 
_temaRepository&& 
=&&  !
new&&" %
TemaRepository&&& 4
(&&4 5
_context&&5 =
)&&= >
;&&> ?
}'' 
return(( 
_temaRepository(( "
;((" #
})) 	
}** 
public,, 

async,, 
Task,, 
CommitAsync,, !
(,,! "
),," #
{-- 
await.. 
_context.. 
... 
SaveChangesAsync.. '
(..' (
)..( )
;..) *
}// 
}00 æ
uD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Services\Token\ITokenService.cs
	namespace 	
BlogPessoal
 
. 
Services 
. 
Token $
;$ %
public 
	interface 
ITokenService 
{ 
JwtSecurityToken 
GenerateAccessToken (
(( )
IEnumerable) 4
<4 5
Claim5 :
>: ;
claims< B
,B C
IConfiguration		) 7
_config		8 ?
)		? @
;		@ A
ClaimsPrincipal

 (
GetPrincipalFromExpiredToken

 0
(

0 1
string

1 7
token

8 =
,

= >
IConfiguration1 ?
_config@ G
)G H
;H I
} ì/
tD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Services\Token\TokenService.cs
	namespace 	
BlogPessoal
 
. 
Services 
. 
Token $
;$ %
public		 
class		 
TokenService		 
:		 
ITokenService		 )
{

 
public 

JwtSecurityToken 
GenerateAccessToken /
(/ 0
IEnumerable0 ;
<; <
Claim< A
>A B
claimsC I
,I J
IConfigurationK Y
_configZ a
)a b
{ 
var 
key 
= 
_config 
. 

GetSection $
($ %
$str% *
)* +
.+ ,
GetValue, 4
<4 5
string5 ;
>; <
(< =
$str= H
)H I
??J L
throw 
new %
InvalidOperationException .
(. /
$str/ G
)G H
;H I
var 

privateKey 
= 
Encoding !
.! "
UTF8" &
.& '
GetBytes' /
(/ 0
key0 3
)3 4
;4 5
var 
signingCredentials 
=  
new! $
SigningCredentials% 7
(7 8
new8 ; 
SymmetricSecurityKey< P
(P Q

privateKeyQ [
)[ \
,\ ]
SecurityAlgorithms! 3
.3 4
HmacSha256Signature4 G
)G H
;H I
var 
tokenDescriptor 
= 
new !#
SecurityTokenDescriptor" 9
{ 	
Subject 
= 
new 
ClaimsIdentity (
(( )
claims) /
)/ 0
,0 1
Expires 
= 
DateTime 
. 
UtcNow %
.% &

AddMinutes& 0
(0 1
_config1 8
.8 9

GetSection9 C
(C D
$strD I
)I J
.0 1
GetValue1 9
<9 :
double: @
>@ A
(A B
$strB Z
)Z [
)[ \
,\ ]
Audience 
= 
_config 
. 

GetSection )
() *
$str* /
)/ 0
. 
GetValue '
<' (
string( .
>. /
(/ 0
$str0 ?
)? @
,@ A
Issuer 
= 
_config 
. 

GetSection '
(' (
$str( -
)- .
.. /
GetValue/ 7
<7 8
string8 >
>> ?
(? @
$str@ M
)M N
,N O
SigningCredentials 
=  
signingCredentials! 3
} 	
;	 

var 
tokenHandler 
= 
new #
JwtSecurityTokenHandler 6
(6 7
)7 8
;8 9
var   
token   
=   
tokenHandler    
.    !"
CreateJwtSecurityToken  ! 7
(  7 8
tokenDescriptor  8 G
)  G H
;  H I
return!! 
token!! 
;!! 
}"" 
public$$ 

ClaimsPrincipal$$ (
GetPrincipalFromExpiredToken$$ 7
($$7 8
string$$8 >
token$$? D
,$$D E
IConfiguration$$F T
_config$$U \
)$$\ ]
{%% 
var&& 
	secretKey&& 
=&& 
_config&& 
[&&  
$str&&  /
]&&/ 0
??&&1 3
throw&&4 9
new&&: =%
InvalidOperationException&&> W
(&&W X
$str&&X h
)&&h i
;&&i j
var(( %
tokenValidationParameters(( %
=((& '
new((( +%
TokenValidationParameters((, E
{)) 	
ValidateAudience** 
=** 
false** $
,**$ %
ValidateIssuer++ 
=++ 
false++ "
,++" #$
ValidateIssuerSigningKey,, $
=,,% &
true,,' +
,,,+ ,
IssuerSigningKey-- 
=-- 
new-- " 
SymmetricSecurityKey--# 7
(--7 8
Encoding.." *
...* +
UTF8..+ /
.../ 0
GetBytes..0 8
(..8 9
	secretKey..9 B
)..B C
)..C D
,..D E
ValidateLifetime// 
=// 
false// $
}00 	
;00	 

var22 
tokenHandler22 
=22 
new22 #
JwtSecurityTokenHandler22 6
(226 7
)227 8
;228 9
var44 
	principal44 
=44 
tokenHandler44 $
.44$ %
ValidateToken44% 2
(442 3
token443 8
,448 9%
tokenValidationParameters44: S
,44S T
out553 6
SecurityToken557 D
securityToken55E R
)55R S
;55S T
if77 

(77 
securityToken77 
is77 
not77  
JwtSecurityToken77! 1
jwtSecurityToken772 B
||77C E
!88 
jwtSecurityToken88 *
.88* +
Header88+ 1
.881 2
Alg882 5
.885 6
Equals886 <
(88< =
SecurityAlgorithms99 +
.99+ ,

HmacSha25699, 6
,996 7
StringComparison:: )
.::) *&
InvariantCultureIgnoreCase::* D
)::D E
)::E F
{;; 	
throw<< 
new<< "
SecurityTokenException<< ,
(<<, -
$str<<- =
)<<= >
;<<> ?
}== 	
return>> 
	principal>> 
;>> 
}?? 
}@@ Â
{D:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Services\Usuario\IUsuariosServices.cs
	namespace 	
BlogPessoal
 
. 
Services 
. 
Usuario &
;& '
public 
	interface 
IUsuariosServices "
{ 
} È
zD:\AceleraMaker\projetosAceleraMaker\Projeto semanas 3 e 4 - Blog Pessoal\BlogPessoal\Services\Usuario\UsuariosServices.cs
	namespace 	
BlogPessoal
 
. 
Services 
; 
public 
class 
UsuariosServices 
: 
IUsuariosServices  1
{ 
} 