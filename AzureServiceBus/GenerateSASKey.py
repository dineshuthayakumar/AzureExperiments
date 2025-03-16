import urllib.parse
import hmac
import hashlib
import base64
import time

def generate_sas_token(uri, key_name, key, expiry_in_seconds=3600):
    # Calculate expiry as Unix time
    expiry = int(time.time() + expiry_in_seconds)
    
    # URL encode the resource URI
    encoded_uri = urllib.parse.quote_plus(uri)
    
    # Create the string to sign
    string_to_sign = encoded_uri + "\n" + str(expiry)
    
    # Compute HMAC-SHA256 signature and then base64 encode it
    key_bytes = key.encode('utf-8')
    message_bytes = string_to_sign.encode('utf-8')
    signature = base64.b64encode(hmac.new(key_bytes, message_bytes, hashlib.sha256).digest())
    
    # URL encode the signature
    encoded_signature = urllib.parse.quote_plus(signature)
    
    # Construct the SAS token
    token = f"SharedAccessSignature sr={encoded_uri}&sig={encoded_signature}&se={expiry}&skn={key_name}"
    return token

# Example usage:
uri = "https://dineshuthayakumar.servicebus.windows.net/kissflowoutbound"
key_name = "Kissflow"  # The shared access policy name
key = ""          # The shared access key
sas_token = generate_sas_token(uri, key_name, key)
print(sas_token)
