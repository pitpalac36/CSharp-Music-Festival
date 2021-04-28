Aplicatii distribuite cross-platform
Notite pt laborator

Google Protobuf
- nu este o tehnologie in sine care sa faciliteze apelurile la distanta (noi scriem codul respectiv), ci are ca focus serializarea obiectelor
- ne permite ca serializarea obiectelor trimise prin socket sa fie independenta de limbaj
Obs. 
     - trebuie precizata pe primul rand al fisierului proto sintaxa folosita pt definirea mesajelor
     - message <=> clasa/structura
     - se serializeaza de fapt tag-urile date si nu numele
     - camp singular : camp care poate sa apara cel mult o data (0 sau 1 ori)
     - camp repeated : camp care poate sa apara de oricate ori (inclusiv 0)
     - might be useful : reserved keyword, nested messages, "any" type, optiunea oneof
     - definirea serviciilor rpc - pt a genera clasele nu ajunge sa folosim compilatorul de la Protobuf, avem nevoie de plugin-uri aditionale
     - java_package - specifica numele pachetului care va fi folosit la generarea claselor Java
     - java_multiple_files : daca il setam true atunci clasele nu vor fi generate la nivel de pachet (altfel : clase nested ale unei singure clase)
protoc --proto_path=IMPORT_PATH --java_out=DST_DIR path/to/file(s).proto
protoc --proto_path=IMPORT_PATH --csharp_out=DST_DIR path/to/file(s).proto
IMPORT_PATH specifica directorul in care compilatorul va cauta fisierele .proto cand incearca sa rezolve optiunile import
(daca nu este specificat se foloseste directorul curent)
pt a genera clasele corespunzatoare : un fisier gen GeneratedClasses.bat (pt Windows) cu o comanda de forma:
	protoc -I=. --java_out=javaFiles --csharp_out=csharpFiles ChatProtocol_v3.proto


gRPC
- se transmite un ProtoRequest; se raspunde cu un ProtoResponse;
- foloseste ProtoBufs pt serializarea/deserializarea obiectelor SI implementeaza de asemenea partea de comunicare
ex.  service SearchService {
	rpc Search (SearchRequest) returns (SearchResponse) ;
     }
- permite specificarea a 4 tipuri de apeluri de metode la distanta : unare, server streaming, client streaming, bidirectional streaming