export function objToQueryString(obj: object) {
    const keyValuePairs = [];
    for (let i = 0; i < Object.keys(obj).length; i += 1) {
        keyValuePairs.push(`${encodeURIComponent(Object.keys(obj)[i])}=${encodeURIComponent(Object.values(obj)[i])}`);
    }
    return '?' + keyValuePairs.join('&');
}

export function decodeJWT(token: string) {
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''))
    return JSON.parse(jsonPayload)
}

export function formatOrderStatus(status: string) {
    const formatter: { [index: string]: string } = {
        'submitted': 'Submitted',
        'awaitingvalidation': 'Awaiting Validation',
        'stockconfirmed': 'Stock Confirmed',
        'shipped': 'Shipped',
        'canceled': 'Canceled'
    }
    return formatter[status];
}