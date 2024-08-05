import argparse
import json
import requests

def create_http_client():
    client = requests.Session()
    client.verify = False
    client.headers.update({"Content-Type": "application/json"})
    return client

def authenticate(edgeip, username, password):
    url = f"https://{edgeip}/api/v1/Login/GetAuthToken/profile"
    data = {
        'Username': username,
        'Password': password
    }

    response = requests.post(url, data=data, verify=False)
    if not response.ok:
        return None
    result = response.json()
    return result

def get_history(edgeip, authtoken, provider_prefix, path):
    url = f"https://{edgeip}/api/history?path={provider_prefix}/{path}"
    response = requests.get(url, verify=False, headers={"Authorization": f"Bearer {authtoken}"})
    
    if not response.ok:
        return None
    result = response.json()
    return result

def main():
    parser = argparse.ArgumentParser(description="Simple CLI app demonstrating how to use DeltaV Edge Environment")
    parser.add_argument("--provider-prefix", help="Provider Prefix")
    parser.add_argument("--path", help="Path")
    parser.add_argument("--edgeip", help="IP Address")
    parser.add_argument("--username", help="Username")
    parser.add_argument("--password", help="Password")
    args = parser.parse_args()

    client = create_http_client()
    auth = authenticate(args.edgeip,args.username, args.password)
    if auth is None:
        print("Invalid Credentials")
        return
    client.headers.update({"Authorization": f"Bearer {auth['accessToken']}"})

    history = get_history(args.edgeip,auth['accessToken'], args.provider_prefix, args.path)
    if history is not None:
        print(f"Latest Value: {history['FieldHistory']['FieldValue'][0]['Value']}")
        print("History Data:")
        for item in history['FieldHistory']['FieldValue']:
           print(item['Value'])

    else:
        print("No history found")

if __name__ == "__main__":
    main()
