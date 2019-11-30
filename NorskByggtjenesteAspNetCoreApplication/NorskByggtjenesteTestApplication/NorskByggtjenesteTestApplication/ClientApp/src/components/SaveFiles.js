import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/SaveFiles';

class SaveFiles extends Component {
    constructor(props) {
        super(props);
        console.log(this.props);
        this.state = {
            files:[],
            currentContent: ''
        }

        this.saveFiles = this.saveFiles.bind(this);
    };

 saveFiles(){
    if (this.state.files.length == 0){
        alert("Must be one file at least");
        return;
    }

    this.props.actions.requestSaveFiles(this.state.files);
    this.setState({
        files:[],
        currentContent: ''
    });
 }

 onChangeValue = event => {
    this.setState({ currentContent: event.target.value });
  };

  onAddItem = () => {
    this.setState(state => {
      const files = this.state.files.concat(this.state.currentContent);
      return {
        files,
        currentContent: '',
      };
    });
  };

  render() {
    return (
      <div>
        <h1>Save Files</h1>
        <p>This component save files written by user.</p>
        <div>
            <textarea className="col-md-11" style={{"height": "200px"}} value={this.state.currentContent} onChange={this.onChangeValue} type="text" />
            <button className="btn btn-success float-md-right" style={{"height": "200px"}} onClick={this.onAddItem}>Add</button>
            <div>
                Number of files in list: <label>{this.state.files.length}</label>
            </div>
        </div>
        
        <button className="btn btn-primary col-md-2" style={{"marginTop": "100px"}} onClick={this.saveFiles}>Save</button>
      </div>
    );
  }
}

const mapDispatchToProps = dispatch => {
    return {
      actions: bindActionCreators(actionCreators, dispatch)
    };
  };
  
  const mapStateToProps = state => {
    return {
      saveFiles: state.saveFiles
    };
  };
  
  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(SaveFiles);
