# Shop

Edit your hosts file. It's location is `C:\Windows\system32\drivers\etc\hosts`.

 Add the following entries:

```
127.0.0.1 skoruba.local sts.skoruba.local admin.skoruba.local admin-api.skoruba.local
```

You'll also need certificates in order to serve on HTTPS. You can make your own self-signed certificates with [mkcert](https://github.com/FiloSottile/mkcert). 

> If the domain is publicly available through DNS, you can use [Let's Encypt](https://letsencrypt.org/).

```powershell
cd Services/Identity/shared/nginx/certs
mkcert --install
copy $env:LOCALAPPDATA\mkcert\rootCA.pem ./cacerts.pem
copy $env:LOCALAPPDATA\mkcert\rootCA.pem ./cacerts.crt
mkcert -cert-file skoruba.local.crt -key-file skoruba.local.key skoruba.local *.skoruba.local
mkcert -pkcs12 skoruba.local.pfx skoruba.local *.skoruba.local
```

Make sure you have installed and configured docker in your environment. After that, you can run the below commands from the base directory
```
docker-compose build
docker-compose up
```
It is also possible to set docker-compose as startup project in Visual Studio and build and run the application with F5 or Ctrl+F5.
