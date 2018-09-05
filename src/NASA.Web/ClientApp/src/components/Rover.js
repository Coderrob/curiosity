import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../store/Rovers";

class Rover extends Component {
  componentWillMount() {
    const name = this.props.name;
    const date = this.props.match.params.date || "";
    this.props.requestRoverPhotos(name, date);
  }

  componentWillReceiveProps(nextProps) {
    const name = nextProps.name;
    const date = nextProps.match.params.date || "";
    this.props.requestRoverPhotos(name, date);
  }

  render() {
    return (
      <div>
        <h1>{this.props.name} Images</h1>

        {renderPhotoTable(this.props)}
      </div>
    );
  }
}

function renderPhotoTable(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Images</th>
        </tr>
      </thead>
      <tbody>
        {props.photos.map(photo => (
          <tr key={photo.id}>
            <td>
              <a href={photo.source} rel="_blank">
                {photo.id}
              </a>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default connect(
  state => state.rovers,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Rover);
