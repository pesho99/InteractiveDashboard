import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import 'bootstrap/dist/css/bootstrap.min.css';
import { PriceUpdate } from '../models';
import TickerComponent from './TickerComponent';
import axios from 'axios';
import ApiService from '../Services/ApiService';


const TickerDisplay: React.FC = () => {

  const [connection, setConnecton] = useState<signalR.HubConnection | null>(null)
  const [allticker, setAllTickers] = useState<string[]>([])
  const [prices, setPrices] = useState<PriceUpdate[]>([]);
  const [tickerNames, setTickerNames] = useState<string[]>([]);

  useEffect(() => {
    connect()
    return () => {
      if (connection == null)
      {
        return
      }
      connection?.stop().then(() => console.log('Connection stopped')).catch((err) => console.error('Error stopping connection: ', err));
      setConnecton(null)
    };
  },[]);

  useEffect(() => {
    let tempPrices = [...prices];
    tickerNames.forEach(ticker => {
      const existingPriceIndex = tempPrices.findIndex(p => p.symbolName === ticker);
      if (existingPriceIndex === -1)
      {
        connection?.invoke("SubscribeForTicker", ticker);
        tempPrices = [...tempPrices, {symbolName: ticker, ask:0, bid:0, askchange:0, bidchange: 0, isConnected:false, priceDate:undefined}]
      }
    });
    tempPrices.forEach(price => {
      const existingNameIndex = tickerNames.findIndex(p => p === price.symbolName);
      if (existingNameIndex === -1)
      {
        connection?.invoke("UnSubscribeForTicker", price.symbolName);
        tempPrices =  tempPrices.filter(p => p !== price)
      }
    });
    setPrices(tempPrices)
  },[tickerNames])

  const connect = async () =>{
    if (connection != null)
    {
      return;
    }
     // Create the SignalR connection
     const newConnection = new signalR.HubConnectionBuilder()
     .withUrl('http://localhost:8080/tickerhub') // replace with your SignalR hub URL
     .withAutomaticReconnect()
     .configureLogging(signalR.LogLevel.Information)
     .build();
     setConnecton(newConnection);

   // Start the SignalR connection
   try
   {
    await newConnection.start();
   }
   catch(err)
   {
      console.error('Connection failed: ', err);
   } 

   // Subscribe to the 'ReceivePriceUpdate' event from the server
   newConnection.on('PriceUpdate', (update: PriceUpdate) => {
     setPrices((prevPrices) => {
       // Check if the symbol is already in the list
       const existingPriceIndex = prevPrices.findIndex(p => p.symbolName === update.symbolName);

       if (existingPriceIndex !== -1) {
         // Update the existing price
         const updatedPrices = [...prevPrices];
         updatedPrices[existingPriceIndex].askchange = update.ask - updatedPrices[existingPriceIndex].ask ;
         updatedPrices[existingPriceIndex].ask = update.ask;
         updatedPrices[existingPriceIndex].bidchange = update.bid - updatedPrices[existingPriceIndex].bid ;
         updatedPrices[existingPriceIndex].bid = update.bid;

         return updatedPrices;
       }
       return prevPrices;
     });
   });
   const tickers = await ApiService.allTickers();
   setAllTickers(tickers.data);
   try
   {
    const personalTickers = await ApiService.getPersonalTickers();
    setTickerNames(personalTickers.data)
   }
   catch(err)
   {
    console.log(err)
   }
  }


  function handleCheckboxChange(id: string): void {
    const existingTickerIndex = tickerNames.findIndex(p => p === id);
    let tempTickerNames = tickerNames;
    if (existingTickerIndex === -1)
    {
      tempTickerNames = [...tickerNames, id]
      setTickerNames(tempTickerNames)
    }
    else
    {
      tempTickerNames = tickerNames.filter((_, index) => index !== existingTickerIndex)
      setTickerNames(tempTickerNames)
    }
    ApiService.updatePersonalTickers(tempTickerNames).catch((err) => console.log(err))
  }

  return (
    <div>
      <h2>Live Price Updates</h2>
      <div className='row'>
        <div className='col-sm-2'>Available tickers:</div>
        {allticker.map((item) => (
          <div key={item} className='col-sm-1'>
            <input
              type="checkbox"
              id={`checkbox-${item}`}
              checked={tickerNames.includes(item)}
              onChange={() => handleCheckboxChange(item)}
            />
            <label htmlFor={`checkbox-${item}`}>{item}</label>
          </div>
        ))}
      </div>
      <div className='row'>
      {prices.map((priceUpdate) => (
              <TickerComponent price={priceUpdate} key={priceUpdate.symbolName} />
          ))}
      </div>
    </div>
  );
};


export default TickerDisplay;
