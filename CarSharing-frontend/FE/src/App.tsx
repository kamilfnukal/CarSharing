import React, { useState } from 'react';
import './App.css';
import LoginPage from './Components/LoginPage/LoginPage';
import HomePage from './Components/HomePage/HomePage'
import { Route, Switch } from 'react-router-dom';
import ProfilePage from './Components/ProfilePage/ProfilePage';
import CreateRidePage from './Components/CreateRidePage/CreateRidePage';
import MainPanel from './Components/MainPanel/MainPanel';
import RidesPage from './Components/RidesPage/RidesPage';
import RatingsPage from './Components/RatingsPage/RatingsPage';
import * as path from './Components/shared/const';
import EnterInfoPage from './Components/LoginPage/EnterPage/EnterInfoPage';
import RiderPage from './Components/RiderPage/RiderPage';
import { Footer } from './Components/Footer/Footer';

export interface IPassenger {
  userId: number,
  rideId: number,
  id: number
}

export interface IPicture {
  id: number,
  url: string
}

export interface ICar {
  brand: string,
  yearOfProduction: number,
  id: number,
  seats: number,
  pictures?: Array<IPicture>
}

export interface IRide {
  availableSeats: number,
  carId: number,
  cityFrom: string,
  cityTo: string,
  dateTime: string,
  driverId: number,
  price: number,
  id: number,
  passengers?: Array<IPassenger> 
}

export interface IRideCreate {
  availableSeats: number,
  carId: number,
  cityFrom: string,
  cityTo: string,
  date: string,
  time: string,
  driverId: number,
  price: number,
  id: number
}

export interface ILoggedUser {
  username: string,
  fullname: string,
  age: number,
  cars: Array<ICar>,
  phoneNumber: string,
  picture?: IPicture,
  id: number,
  rides?: Array<IRide>
}

export interface IPage {
  user: ILoggedUser | undefined
}

function App() {

  const [user, setUser] = useState<ILoggedUser>();
  const [password, setPassword] = useState<string>("");

  const [driverId, setDriverId] = useState(-1); // does not work

  return (
    <>    
      <Switch>
        <Route path={path.LOGIN_PATH}>
          <LoginPage user={user} setUser={(e: any) => setUser(e)} setPassword={setPassword} />
          <Footer />
        </Route> 
        <Route path={path.PROFILE_PATH}>
          <MainPanel />
          <ProfilePage user={user} setUser={setUser}/>
          <Footer />
        </Route>
        <Route path={path.HOME_PATH}>
          <MainPanel />
          <HomePage user={user!} setDriverId={setDriverId} setUser={setUser}/>
          <Footer />
        </Route>
        <Route path={path.CREATE_RIDE_PATH}>
          <MainPanel />
          <CreateRidePage user={user}/>
          <Footer />
        </Route>
        <Route path={path.MY_RIDES_PATH}>
          <MainPanel />
          <RidesPage user={user} />
          <Footer />
        </Route>
        <Route path={path.MY_RATINGS_PATH}>
          <MainPanel />
          <RatingsPage user={user}/>
          <Footer />
        </Route>
        <Route path={path.ENTER_INFO_PATH}>
          <EnterInfoPage user={user!} setUser={setUser} password={password}/>
          <Footer />
        </Route>
        <Route path={path.DRIVER_PATH}>
          <RiderPage driverId={driverId}/>
          <Footer />
        </Route>
      </Switch>
    </>
  );
}

export default App;
