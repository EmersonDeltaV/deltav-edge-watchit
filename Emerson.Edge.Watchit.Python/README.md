# How to Use Emerson.Edge.Watchit.Python.py

Follow these steps to use the `Emerson.Edge.Watchit.Python.py` script:

## Prerequisites

- Python installed on your machine
- Required Python packages: `argparse`, `json`, `requests`

## Steps

1. **Clone the Repository**: Open a terminal and run the following command to clone the repository:
    ```sh
    git clone https://github.com/EmersonDeltaV/Emerson.Edge.Watchit
    ```

2. **Navigate to the Directory**: Change to the directory where `Emerson.Edge.Watchit.Python.py` is located:
    ```sh
    cd Emerson.Edge.Watchit/Emerson.Edge.Watchit.Python
    ```

3. **Install Dependencies**: Install the required dependencies by running:
    ```sh
    pip install requests
    ```

4. **Run the Script**: Execute the script with the required arguments:
    ```sh
    python Emerson.Edge.Watchit.Python.py --provider-prefix <provider_prefix> --path <path> --edgeip <edge_ip> --username <username> --password <password>
    ```

    Replace `<provider_prefix>`, `<path>`, `<edge_ip>`, `<username>`, and `<password>` with the appropriate values.

    Example:
    ```sh
    python Emerson.Edge.Watchit.Python.py --provider-prefix "example_prefix" --path "example_path" --edgeip "192.168.1.1" --username "user" --password "pass"
    ```

## Script Overview

The script performs the following actions:

1. **Create HTTP Client**: Initializes an HTTP client with necessary headers.
    ```python
    def create_http_client():
        client = requests.Session()
        client.verify = False
        client.headers.update({"Content-Type": "application/json"})
        return client
    ```

2. **Authenticate**: Authenticates with the DeltaV Edge system using provided credentials.
    ```python
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
    ```

3. **Get History**: Retrieves historical data for a specified path.
    ```python
    def get_history(edgeip, authtoken, provider_prefix, path):
        url = f"https://{edgeip}/api/history?path={provider_prefix}/{path}"
        response = requests.get(url, verify=False, headers={"Authorization": f"Bearer {authtoken}"})
        
        if not response.ok:
            return None
        result = response.json()
        return result
    ```

4. **Main Function**: Parses command-line arguments and calls the appropriate functions.
    ```python
    def main():
        parser = argparse.ArgumentParser(description="Simple CLI app demonstrating how to use DeltaV Edge Environment")
        parser.add_argument("--provider-prefix", help="Provider Prefix")
        parser.add_argument("--path", help="Path")
        parser.add_argument("--edgeip", help="IP Address")
        parser.add_argument("--username", help="Username")
        parser.add_argument("--password", help="Password")
    ```

Follow these instructions to successfully run the `Emerson.Edge.Watchit.Python.py` script and interact with the DeltaV Edge system.