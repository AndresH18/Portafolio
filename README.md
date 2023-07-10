# Portafolio
This is my Portafolio Proyect

## Library Manager
### Check if Library Manager is installed
```ps
dotnet tool list --global

Package Id                            Version      Commands
-----------------------------------------------------------
microsoft.web.librarymanager.cli      2.1.175      libman
```
### Install Library Manager
```ps
dotnet tool install --global Microsoft.Web.librarymManager.Cli --version 2.1.175
```
### Initialize Libman
```ps
cd .\MyPortafolio\

libman init -p cdnjs
```

## Libraries
To install the libraries correctly, you muse be inside `.\MyPortafolio\`


### Bootstrap
```ps
libman install bootstrap@5.2.3 -d wwwroot/lib/bootstrap
```

### Font-Awesome
```ps 
libman install font-awesome@6.2.1 -d wwwroot/lib/font-awesome
```

