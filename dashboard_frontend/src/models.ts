export interface PriceUpdate {
    symbolName: string,
    ask: number;
    askchange: number;
    bid: number;
    bidchange: number;
    priceDate: Date | undefined;
    isConnected: boolean;
}