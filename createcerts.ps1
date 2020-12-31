# http://woshub.com/how-to-create-self-signed-certificate-with-powershell/ 
# https://docs.microsoft.com/en-us/powershell/module/pkiclient/new-selfsignedcertificate?view=win10-ps#inputs
# https://docs.microsoft.com/en-us/powershell/module/pkiclient/get-certificate?view=win10-ps
# You can create a certificate chain. First, a root certificate (CA) is created, and based on it, an SSL server certificate is generated:
$rootCert = New-SelfSignedCertificate -Subject 'CN=Z-710RootCA,O=Z-710RootCA,OU=Z-710RootCA' -KeyExportPolicy Exportable  -KeyUsage CertSign,CRLSign,DigitalSignature -KeyLength 2048 -KeyUsageProperty All -KeyAlgorithm 'RSA'  -HashAlgorithm 'SHA256'  -Provider 'Microsoft Enhanced RSA and AES Cryptographic Provider'
New-SelfSignedCertificate -CertStoreLocation "cert:\CurrentUser\My" -DnsName "test.Z-710.com" -Signer $rootCert -KeyUsage KeyEncipherment,DigitalSignature
